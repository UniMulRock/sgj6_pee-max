using Utility.System;
using UnityEngine;
using PeeMax.Char;
using PeeMax.Stage;
using UnityStandardAssets.Cameras;

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

        [SerializeField]
        private AutoCam autoCam;

        public Transform CamTarget
        {
            get{ return autoCam.Target; }
            set{ autoCam.SetTarget(value); }
        }

        protected override void Awake()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.WindowRoot = windowRoot;
            }

            if (CharGenerater.Validation())
            {
                var character = CharGenerater.Instance.CreateChar(charRoot.transform);
				CamTarget = charRoot.transform;//character.transform;
            }

            if (StageGenerater.Validation())
            {
                StageGenerater.Instance.CreateStage(stageRoot.transform,"test1");
            }
        }
    }
}