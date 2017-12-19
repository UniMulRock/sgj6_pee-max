using Utility.System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using PeeMax.Stage;
using PeeMax.Icon;

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
        /// ステージ選択ページのルート
        /// </summary>
        [SerializeField]
        private Transform StageSelectRoot;

        /// <summary>
        /// ステージデータのリスト
        /// </summary>
        [SerializeField]
        private List<StageData> stageDataList = new List<StageData>();

        [SerializeField]
        private string bgmName;

        private Dictionary<int, List<StageData>> stageButtonList = new Dictionary<int, List<StageData>>();

        private Dictionary<int, Transform> stageSelectPageDic = new Dictionary<int, Transform>();

        private Dictionary<int, List<StageSelectButton>> stageSelectButtonDic = new Dictionary<int, List<StageSelectButton>>();

        public StageData CurrentStageData{ set; get; }

        protected override void Awake()
        {
			base.Awake ();
			GameObject.DontDestroyOnLoad(this.gameObject);

            if (WindowManager.Validation())
            {
                WindowManager.Instance.WindowRoot = windowRoot;
            }

            // StageDataの取得
            using (var reader = new CSVReader<StageData>(StageDataDefine.CSV_DIC + StageDataDefine.STAGE_DATA_CSV, true))
            {
                stageDataList = reader.ToList();
                stageDataList.ForEach((data) =>
                    {
                        CreateStageSelect(data);
                        Debug.Log(data.ToString());
                    });
            }
        }

        private void Start()
        {
            Sound.PlayBgm(bgmName);
        }

        public void OnSetting()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.OpenWindow(WindowManager.WINDOW_TYPE.SETTING);
            }
        }

        public void NextScene()
        {
            SceneManager.Instance.ChangeState(SceneManager.STATE.GAME);
            Sound.StopBgm();
        }

        #region ステージ選択部作成

        private void CreateStageSelect(StageData data)
        {
            if (stageButtonList == null)
            {
                stageButtonList = new Dictionary<int, List<StageData>>();
                stageSelectButtonDic = new Dictionary<int, List<StageSelectButton>>();
            }

            if (stageButtonList.Keys.Count <= 0 || stageButtonList[stageButtonList.Keys.Last()].Count() >= StageDataDefine.STAGE_SELECT_BUTTOM_MAX)
            {
                CreateStagePage(stageButtonList.Keys.Count);
            }

            CreateStageButton
            (
                stageButtonList.Keys.Count - 1,
                stageButtonList[stageButtonList.Keys.Count - 1].Count,
                data
            );
        }

        private void CreateStagePage(int pageValue)
        {
            stageButtonList.Add(pageValue, new List<StageData>());
            stageSelectButtonDic.Add(pageValue, new List<StageSelectButton>());

            var stageListRoot = UnityEngine.Resources.Load(StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.STAGE_LIST_ROOT) as GameObject;
            if (stageListRoot == null)
            {
                Debug.LogError("【ロード失敗】" + StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.STAGE_LIST_ROOT);
                return;
            }
            var instanceStageListRoot = Instantiate(stageListRoot, StageSelectRoot) as GameObject;
            if (instanceStageListRoot == null)
            {
                Debug.LogError("【生成失敗】" + StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.STAGE_LIST_ROOT);
                return;
            }
            instanceStageListRoot.name = StageDataDefine.STAGE_LIST_ROOT + "_" + pageValue.ToString();
            stageSelectPageDic.Add(pageValue, instanceStageListRoot.transform);
        }

        private void CreateStageButton(int pageValue, int buttonValue, StageData data)
        {
            if (data.IconList == null)
            {
                data.IconList = new List<Sprite>();
            }

            if (IconManager.Validation())
            {
                for (int i = 0; i < data.IconUniqueIdArray.Length; i++)
                {
                    data.IconList.Add(IconManager.Instance.GetIconSprite(data.IconUniqueIdArray[i]));
                }
            }

            stageButtonList[pageValue].Add(data);
            var stageButton = UnityEngine.Resources.Load(StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.STAGE_SELECT_BUTTON) as GameObject;
            if (stageButton == null)
            {
                Debug.LogError("【ロード失敗】" + StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.STAGE_SELECT_BUTTON);
                return;
            }
            var instanceStageButton = Instantiate(stageButton, stageSelectPageDic[pageValue]) as GameObject;
            if (instanceStageButton == null)
            {
                Debug.LogError("【生成失敗】" + StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.STAGE_SELECT_BUTTON);
                return;
            }
            instanceStageButton.name = StageDataDefine.STAGE_SELECT_BUTTON + "_" + buttonValue.ToString();
            var instanceStageButtonSrc = instanceStageButton.GetComponent<StageSelectButton>();
            if (instanceStageButtonSrc == null)
            {
                instanceStageButtonSrc = instanceStageButton.AddComponent<StageSelectButton>();
            }
            instanceStageButtonSrc.CurrentStageDate = data;
            stageSelectButtonDic[pageValue].Add(instanceStageButtonSrc);
            instanceStageButtonSrc.Setup();
        }

        #endregion
    }
}