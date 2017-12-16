using Utility.System;
using UnityEngine;

namespace PeeMax.Char
{
    public class CharGenerater : SingletonMonoBehaviour<CharGenerater>
    {
        private const string CHAR_DIC = "Char/";

        private const string CHAR_DATA = "UTC_Default";

        public CharController CreateChar(Transform root)
        {
            var characterResource = Resources.Load(CHAR_DIC + CHAR_DATA) as GameObject;
            if (characterResource == null)
            {
                Debug.LogError("【ロード失敗】" + CHAR_DIC + CHAR_DATA);
                return null;
            }
            var instanceCharacter = Instantiate(characterResource, root) as GameObject;
            if (instanceCharacter == null)
            {
                Debug.LogError("【生成失敗】" + CHAR_DIC + CHAR_DATA);
                return null;
            }
            instanceCharacter.name = CHAR_DATA;
            var controller = instanceCharacter.GetComponent<CharController>();
            if (controller == null)
            {
                controller = instanceCharacter.AddComponent<CharController>();
            }
            return controller;
        }
    }
}