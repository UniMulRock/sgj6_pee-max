﻿using Utility.System;
using Utility.System.Data;
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

        [SerializeField]
        private string bgmName;

		public string DebugStageName = "stage1";

        public Transform CamTarget
        {
            get{ return autoCam.Target; }
            set{ autoCam.SetTarget(value); }
        }

		public GameObject CharRootObject
		{
			get { return charRoot; }
		}

        protected override void Awake()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.WindowRoot = windowRoot;
            }

            if (CharGenerater.Validation())
            {
                CharGenerater.Instance.CreateChar(charRoot.transform);
				CamTarget = charRoot.transform;
            }

			if (StageGenerater.Validation())
            {
				string stageDataName = DebugStageName;
				if (StageSelectSceneSystem.Validation ()) {
					stageDataName = StageSelectSceneSystem.Instance.CurrentStageData.StageDataName;
				}
				StageGenerater.Instance.CreateStage(stageRoot.transform, stageDataName );
            }
        }

        private void Start()
        {
            Sound.PlayBgm(bgmName);
        }

        public void GoToNextScene()
        {
            if (SaveManager.Validation())
            {
                var clearedId = SaveManager.Instance.Load();
                if (clearedId > 0)
                {
                    SaveData saveData = new SaveData();
                    saveData.stageUniqueId = 0;
                    // オートセーブ
                    SaveManager.Instance.Save(saveData);
                }
            }
            SceneManager.Instance.ChangeState(SceneManager.STATE.STAGE_SELECT);
            Sound.StopBgm();
        }
    }
}