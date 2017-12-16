using Utility.System;
using UnityEngine;

namespace PeeMax.Stage
{
    public class StageGenerater : SingletonMonoBehaviour<StageGenerater>
    {
        private const string STAGE_DIC = "Stage/";

        public GameObject CreateStage(Transform root, string stageName)
        {
            var characterResource = Resources.Load(STAGE_DIC + stageName) as GameObject;
            if (characterResource == null)
            {
                Debug.LogError("【ロード失敗】" + STAGE_DIC + stageName);
                return null;
            }
            var instanceCharacter = Instantiate(characterResource, root) as GameObject;
            if (instanceCharacter == null)
            {
                Debug.LogError("【生成失敗】" + STAGE_DIC + stageName);
                return null;
            }
            instanceCharacter.name = stageName;
            return instanceCharacter;
        }
    }
}