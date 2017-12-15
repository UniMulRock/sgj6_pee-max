using Utility.System;
using UnityEngine;

namespace System.Scene
{
    public class StageSelectSceneSystem : SingletonMonoBehaviour<StageSelectSceneSystem>
    {
        /// <summary>
        /// WindowRoot
        /// </summary>
        [SerializeField]
        private GameObject windowRoot;

        protected override void Awake()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.WindowRoot = windowRoot;
            }
        }

        public void OnSetting()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.OpenWindow(WindowManager.WINDOW_TYPE.SETTING);
            }
        }
    }
}