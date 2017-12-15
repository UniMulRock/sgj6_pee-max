using Utility.System;
using UnityEngine;
using System.Scene;
using System;
using PeeMax.Stage;
using UnityEngine.UI;

namespace Utility.Window
{
    public class StageWindow : WindowBase
    {
        [SerializeField]
        private Text stageNameText;

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
                    if(callback != null)
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
            if (data == null)
                return;

            if (stageNameText != null)
            {
                stageNameText.text = data.StageName;
            }
        }
    }
}
