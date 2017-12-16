using Utility.System;
using UnityEngine;
using PeeMax.Char;
using PeeMax.Stage;

namespace System.Scene
{
    public class GameSceneSystem : SingletonMonoBehaviour<GameSceneSystem>
    {
        /// <summary>
        /// WindowRoot
        /// </summary>
        [SerializeField]
        private GameObject windowRoot;

        [SerializeField]
        private GameObject stageRoot;

        [SerializeField]
        private GameObject charRoot;

        protected override void Awake()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.WindowRoot = windowRoot;
            }

            if (CharGenerater.Validation())
            {
                CharGenerater.Instance.CreateChar(charRoot.transform);
            }

            if (StageGenerater.Validation())
            {
                StageGenerater.Instance.CreateStage(stageRoot.transform,"test1");
            }
        }
    }
}