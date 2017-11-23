using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    //再生ボタンを押さなくてもスクリプト実行する
    [ExecuteInEditMode]
    //コンポーネントメニューに項目追加
    [AddComponentMenu ("Image Effects/Blur/Blur (Optimized)")]

    public class BlurOptimized : PostEffectsBase
    {
        //パラメータの範囲を0-2に制限
        [Range(0, 2)]
        //ぼかしをかける前に画像をどれだけ縮小するか
        //1/(2^downsample)サイズに縮小
        public int downsample = 1;

        [Range(0.0f, 10.0f)]
        //ぼかし範囲 シェーダーの_Parameterに渡す
        public float blurSize = 3.0f;

        [Range(1, 4)]
        //何回ぼかし処理を繰り返すか
        public int blurIterations = 2;

        //シェーダー
        public Shader blurShader = null;
        //シェーダーを反映するマテリアル
        private Material blurMaterial = null;

        //ブラー使用可能ならマテリアル準備
        public override bool CheckResources () {
            CheckSupport (false);

            blurMaterial = CheckShaderAndCreateMaterial (blurShader, blurMaterial);

            if (!isSupported)
                ReportAutoDisable ();
            return isSupported;
        }
        //無効状態になったらマテリアルを即座に破棄
        public void OnDisable () {
            if (blurMaterial)
                DestroyImmediate (blurMaterial);
        }
        //全てのレンダリングが完了し RenterTexture にレンダリングされた後に呼び出される
        public void OnRenderImage (RenderTexture source, RenderTexture destination) {
            if (CheckResources() == false) {
                Graphics.Blit (source, destination);
                return;
            }

            //画面何分の一？
            float widthMod = 1.0f / (1.0f * (1<<downsample));
            //原画像バイリニア補間
            source.filterMode = FilterMode.Bilinear;
            //縮小画像サイズ
            int rtW = source.width >> downsample;
            int rtH = source.height >> downsample;

            //画像縮小のため一時テクスチャを確保
            RenderTexture rt = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
            //一時テクスチャもバイリニア補間
            rt.filterMode = FilterMode.Bilinear;
            //シェーダーパス0で元画像を縮小して一時テクスチャに転送
            Graphics.Blit (source, rt, blurMaterial, 0);
            //指定回数ぼかし処理を行う
            for(int i = 0; i < blurIterations; i++) {
                //ぼかしサンプリング距離。繰り返し毎に広げる
                float iterationOffs = (i*1.0f);
                //シェーダーにサンプリング距離設定。xだけ使う
                blurMaterial.SetVector ("_Parameter", new Vector4 (blurSize * widthMod + iterationOffs, -blurSize * widthMod - iterationOffs, 0.0f, 0.0f));

                // 垂直方向ぼかし結果保存のため一時テクスチャ確保
                RenderTexture rt2 = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
                rt2.filterMode = FilterMode.Bilinear;
                // ぼかし処理
                Graphics.Blit (rt, rt2, blurMaterial, 1);
                // 垂直ぼかしができたため原画像は不要。縮小画像一時テクスチャ解放
                RenderTexture.ReleaseTemporary (rt);
                rt = rt2;

                // 水平方向ぼかし。垂直方向と同様
                rt2 = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
                rt2.filterMode = FilterMode.Bilinear;
                Graphics.Blit (rt, rt2, blurMaterial, 2);
                RenderTexture.ReleaseTemporary (rt);
                rt = rt2;
            }

            //ぼかしが終わったので結果テクスチャに転送
            Graphics.Blit (rt, destination);
            //一時テクスチャ解放
            RenderTexture.ReleaseTemporary (rt);
        }
    }
}