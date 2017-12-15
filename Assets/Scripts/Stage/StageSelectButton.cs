using UnityEngine;
using UnityEngine.UI;
using Utility.System;
using System.Scene;

namespace PeeMax.Stage
{
    public class StageSelectButton : MonoBehaviour
    {
        private StageData currentStageDate;

        public StageData CurrentStageDate{ set{ currentStageDate = value; } }

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Text buttonText;

        public void Setup()
        {
            if (currentStageDate == null)
                return;

            if (backgroundImage != null)
            {
                backgroundImage.color = Color.white;
            }

            if (buttonText != null)
            {
                buttonText.text = currentStageDate.StageName;
            }
        }

        public void OnClick()
        {
            if (currentStageDate == null)
                return;

            if (StageSelectSceneSystem.Validation())
            {
                StageSelectSceneSystem.Instance.CurrentStageData = currentStageDate;
            }

            if (WindowManager.Validation())
            {
                WindowManager.Instance.OpenWindow(WindowManager.WINDOW_TYPE.STAGE);
            }
            Debug.Log(currentStageDate.ToString());
            return;
        }
    }
}