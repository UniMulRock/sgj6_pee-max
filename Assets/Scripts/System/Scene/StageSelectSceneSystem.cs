using Utility.System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using PeeMax.Stage;

namespace System.Scene
{
    public class StageSelectSceneSystem : SingletonMonoBehaviour<StageSelectSceneSystem>
    {
        /// <summary>
        /// WindowRoot
        /// </summary>
        [SerializeField]
        private GameObject windowRoot;

        /// <summary>
        /// ステージデータのリスト
        /// </summary>
        [SerializeField]
        private List<StageData> stageDataList = new List<StageData>();

        protected override void Awake()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.WindowRoot = windowRoot;
            }
        }

        private void Start()
        {
            // StageDataの取得
            using (var reader = new CSVReader<StageData>(StageDataDefine.CSV_DIC + StageDataDefine.STAGE_DATA_CSV, true))
            {
                stageDataList = reader.ToList();
                stageDataList.ForEach((data) =>
                {
                    Debug.Log(data.ToString());
                });
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