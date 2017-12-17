using UnityEngine.Rendering;
using Utility.System;
using UnityEngine;

namespace System.Scene
{
    public class EntryPointSystem : SingletonMonoBehaviour<EntryPointSystem>
    {
        /// <summary>
        /// シーン遷移の処理したか
        /// </summary>
        private bool isNextScene;

        /// <summary>
        /// 最初のフレームのアップデート前に呼ばれます.
        /// </summary>
        void Start()
        {
            isNextScene = false;
        }

        /// <summary>
        /// 毎フレーム呼び出されます.
        /// </summary>
        void Update()
        {
            if (!SplashScreen.isFinished || isNextScene)
                return;
            
            isNextScene = true;
            //スプラッシュ表示後
            NextScene();
        }

        /// <summary>
        /// 次のシーンへ遷移
        /// </summary>
        private void NextScene()
        {
            SceneManager.Instance.ChangeState(SceneManager.STATE.TITLE_CREDIT);
        }
    }
}
