// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Gauss"  
  {
        Properties
        {
            _MainTex ("Texture", 2D) = "white" {}
                radius ("radius", Range(0,30)) =0
                resolution ("resolution", float) = 800 
                _Hue ("Hue", Float) = 0         //色相
 _Sat ("Saturation", Float) = 1  //彩度
 _Val ("Value", Float) = 1       //明度
        }
        SubShader
        {
            Tags {
            "Queue"="Geometry"
             "RenderType"="Opaque"
             }
            LOD 100

            Pass
            {
            Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "CanUseSpriteAtlas"="true" "PreviewType"="Plane" }
  ZWrite Off
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag       
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                half _Hue, _Sat, _Val;

                uniform  float resolution = 800;
                uniform  float radius = 400;
                uniform  float2 dir = float2(0,1);

                v2f vert (appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed3 shift_col(fixed3 RGB, half3 shift)
     {
     fixed3 RESULT = fixed3(RGB);
     float VSU = shift.z*shift.y*cos(shift.x*3.14159265/180);
       float VSW = shift.z*shift.y*sin(shift.x*3.14159265/180);
         
       RESULT.x = (.299*shift.z+.701*VSU+.168*VSW)*RGB.x
           + (.587*shift.z-.587*VSU+.330*VSW)*RGB.y
           + (.114*shift.z-.114*VSU-.497*VSW)*RGB.z;
         
       RESULT.y = (.299*shift.z-.299*VSU-.328*VSW)*RGB.x
           + (.587*shift.z+.413*VSU+.035*VSW)*RGB.y
           + (.114*shift.z-.114*VSU+.292*VSW)*RGB.z;
         
       RESULT.z = (.299*shift.z-.3*VSU+1.25*VSW)*RGB.x
           + (.587*shift.z-.588*VSU-1.05*VSW)*RGB.y
           + (.114*shift.z+.886*VSU-.203*VSW)*RGB.z;
         
     return (RESULT);
     }

                fixed4 frag (v2f i) : SV_Target
        {                   
                float4 sum = float4(0.0, 0.0, 0.0, 0.0);
                float2 tc = i.uv;

                //blur radius in pixels
                float blur = radius/resolution/4; 

                //the direction of our blur
                //(1.0, 0.0) -> x-axis blur
                //(0.0, 1.0) -> y-axis blur

                float hstep = 1;
                float vstep = 0;

                sum += tex2D(_MainTex, float2(tc.x - 4.0*blur*hstep, tc.y - 4.0*blur*vstep)) * 0.0162162162;
                sum += tex2D(_MainTex, float2(tc.x - 3.0*blur*hstep, tc.y - 3.0*blur*vstep)) * 0.0540540541;
                sum += tex2D(_MainTex, float2(tc.x - 2.0*blur*hstep, tc.y - 2.0*blur*vstep)) * 0.1216216216;
                sum += tex2D(_MainTex, float2(tc.x - 1.0*blur*hstep, tc.y - 1.0*blur*vstep)) * 0.1945945946;

                sum += tex2D(_MainTex, float2(tc.x, tc.y)) * 0.2270270270;

                sum += tex2D(_MainTex, float2(tc.x + 1.0*blur*hstep, tc.y + 1.0*blur*vstep)) * 0.1945945946;
                sum += tex2D(_MainTex, float2(tc.x + 2.0*blur*hstep, tc.y + 2.0*blur*vstep)) * 0.1216216216;
                sum += tex2D(_MainTex, float2(tc.x + 3.0*blur*hstep, tc.y + 3.0*blur*vstep)) * 0.0540540541;
                sum += tex2D(_MainTex, float2(tc.x + 4.0*blur*hstep, tc.y + 4.0*blur*vstep)) * 0.0162162162;
                //sum.rgb = dot(sum.rgb, float3(0.3f, 0.2f, 0.4f));
                half3 shift = half3(_Hue, _Sat, _Val);

                return fixed4( shift_col(sum, shift), sum.a);
        }

                ENDCG
            }
        }
    }