using Utility.System;
using UnityEngine;
using System.Scene;
using System;
using PeeMax.Stage;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Utility.Window
{
    public class StageWindow : WindowBase
    {
        [SerializeField]
        private Text stageNameText;

        [SerializeField]
        private GameObject iconRoot;

        private List<GameObject> iconObjList = new List<GameObject>();

        public void OnClose()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.CloseWindow(WindowManager.WINDOW_TYPE.STAGE);
            }
        }

        public void OnStartStage()
        {
            if (StageSelectSceneSystem.Validation())
            {
                StageSelectSceneSystem.Instance.NextScene();
            }
        }

        public override void Close(Action callback = null)
        {
            base.Close(() =>
                {
                    if (StageSelectSceneSystem.Validation())
                    {
                        StageSelectSceneSystem.Instance.CurrentStageData = null;
                    }
                    if (callback != null)
                    {
                        callback();
                    }
                });
        }

        public override void Open(Action callback = null)
        {
            Setup(StageSelectSceneSystem.Instance.CurrentStageData);
            base.Open(callback);
        }

        public void Setup(StageData data)
        {
            if (iconObjList == null)
            {
                iconObjList = new List<GameObject>();
            }
            else
            {
                iconObjList.ForEach((image) =>
                    {
                        Destroy(image.gameObject);
                    });
                iconObjList.Clear();
            }


            if (data == null)
                return;

            if (stageNameText != null)
            {
                stageNameText.text = data.StageName;
            }

            if (iconRoot != null)
            {
                data.IconList.ForEach((sprite) =>
                    {
                        var imageObj = CreateIcon(sprite);
                        if (imageObj != null)
                        {
                            iconObjList.Add(imageObj);
                        }
                    });
            }
        }

        private GameObject CreateIcon(Sprite sprite)
        {
            var iconImageRsource = UnityEngine.Resources.Load(StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.ICON_IMAGE) as GameObject;
            if (iconImageRsource == null)
            {
                Debug.LogError("【ロード失敗】" + StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.ICON_IMAGE);
                return null;
            }
            var instanceIconImage = Instantiate(iconImageRsource, iconRoot.transform) as GameObject;
            if (instanceIconImage == null)
            {
                Debug.LogError("【生成失敗】" + StageDataDefine.STAGE_SELECT_DIC + StageDataDefine.ICON_IMAGE);
                return null;
            }
            instanceIconImage.name = StageDataDefine.ICON_IMAGE + "_" + sprite.name;
            var instanceIconImageSrc = instanceIconImage.transform.GetChild(0).GetComponentInChildren<Image>();
            if (instanceIconImageSrc == null)
                return null;
            instanceIconImageSrc.sprite = sprite;
            return instanceIconImage;
        }
    }
}
