using Utility.System;
using UnityEngine;

namespace System.Scene
{
    public class TitleCreditSystem : MonoBehaviour
    {
        /// <summary>
        /// 次のシーンへ遷移
        /// </summary>
        public void NextScene()
        {
            SceneManager.Instance.ChangeState(SceneManager.STATE.TITLE);
        }
    }
}