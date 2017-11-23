// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "custom/FastBlur" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    //入力画像 Unityから自動で渡される
    sampler2D _MainTex;
    //入力画像1ピクセルあたりの幅高さ
    uniform half4 _MainTex_TexelSize;
    //サンプリング距離 何画素間隔？
    uniform half4 _Parameter;

    //画像縮小 頂点シェーダー出力
    struct v2f_tap
    {
        float4 pos : SV_POSITION;
        half2 uv20 : TEXCOORD0;
        half2 uv21 : TEXCOORD1;
        half2 uv22 : TEXCOORD2;
        half2 uv23 : TEXCOORD3;
    };          
    //画像縮小 頂点シェーダー
    v2f_tap vert4Tap ( appdata_img v )
    {
        v2f_tap o;

        o.pos = UnityObjectToClipPos (v.vertex);
        //画像サンプリング位置
        o.uv20 = v.texcoord + _MainTex_TexelSize.xy;                
        o.uv21 = v.texcoord + _MainTex_TexelSize.xy * half2(-0.5h,-0.5h);   
        o.uv22 = v.texcoord + _MainTex_TexelSize.xy * half2(0.5h,-0.5h);        
        o.uv23 = v.texcoord + _MainTex_TexelSize.xy * half2(-0.5h,0.5h);        

        return o; 
    }                   
    //画像縮小フラグメントシェーダー 4点サンプリングして平均色を出力する
    fixed4 fragDownsample ( v2f_tap i ) : SV_Target
    {               
        fixed4 color = tex2D (_MainTex, i.uv20);
        color += tex2D (_MainTex, i.uv21);
        color += tex2D (_MainTex, i.uv22);
        color += tex2D (_MainTex, i.uv23);
        return color / 4;
    }

    // 画素にかける係数 ガウス関数から算出
    static const half curve[7] = { 0.0205, 0.0855, 0.232, 0.324, 0.232, 0.0855, 0.0205 };  // gauss'ish blur weights
    //頂点シェーダー出力
    struct v2f_withBlurCoords8 
    {
        float4 pos : SV_POSITION;
        half4 uv : TEXCOORD0;
        //uv値に直したサンプリング間隔
        half2 offs : TEXCOORD1;
    };  

    //水平方向ガウシアンブラー頂点シェーダー
    v2f_withBlurCoords8 vertBlurHorizontal (appdata_img v)
    {
        v2f_withBlurCoords8 o;
        o.pos = UnityObjectToClipPos (v.vertex);

        o.uv = half4(v.texcoord.xy,1,1);
        //1ピクセルあたりの大きさ*サンプリング画素間隔でuvオフセット値を計算しておいてフラグメントシェーダーに渡す
        o.offs = _MainTex_TexelSize.xy * half2(1.0, 0.0) * _Parameter.x;

        return o; 
    }
    //垂直報告ガウシアンブラー 頂点シェーダー
    v2f_withBlurCoords8 vertBlurVertical (appdata_img v)
    {
        v2f_withBlurCoords8 o;
        o.pos = UnityObjectToClipPos (v.vertex);

        o.uv = half4(v.texcoord.xy,1,1);
        o.offs = _MainTex_TexelSize.xy * half2(0.0, 1.0) * _Parameter.x;

        return o; 
    }   
    //ガウシアンブラーフラグメントシェーダー
    half4 fragBlur8 ( v2f_withBlurCoords8 i ) : SV_Target
    {
        half2 uv = i.uv.xy; 
        half2 netFilterWidth = i.offs;  
        half2 coords = uv - netFilterWidth * 3.0;  

        half4 color = 0;
        //８画素係数をかけて足す
        for( int l = 0; l < 7; l++ )  
        {   
            half4 tap = tex2D(_MainTex, coords);
            color += tap * curve4[l];
            coords += netFilterWidth;
        }
        return color;
    }
    ENDCG

    SubShader {
        ZTest Off Cull Off ZWrite Off Blend Off
        // パス0 画像縮小
        Pass { 
            CGPROGRAM

            #pragma vertex vert4Tap
            #pragma fragment fragDownsample

            ENDCG
        }

        // パス1 垂直方向処理
        Pass {
            ZTest Always
            Cull Off

            CGPROGRAM 

            #pragma vertex vertBlurVertical
            #pragma fragment fragBlur8

            ENDCG 
        }   

        // パス2 水平方向処理
        Pass {      
            ZTest Always
            Cull Off

            CGPROGRAM

            #pragma vertex vertBlurHorizontal
            #pragma fragment fragBlur8

            ENDCG
        }   
        FallBack Off
    }
}