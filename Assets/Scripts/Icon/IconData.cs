using System;
using UnityEngine;
using PeeMax.Stage;

namespace PeeMax.Icon
{
    [Serializable]
    public class IconData
    {
        [CsvColumnAttribute(0)]
        [SerializeField]
        private int uniqueId;

        [CsvColumnAttribute(1)]
        [SerializeField]
        private string iconName;

        [CsvColumnAttribute(2)]
        [SerializeField]
        private string resourceName;

        private Sprite imageResource;

        public string IconName{ get { return iconName; } }

        public string ResourceName{ get { return resourceName; } }

        public void SetImageResource()
        {
            var imageResourceData = UnityEngine.Resources.Load<Sprite>(StageDataDefine.IMAGE_DIC + StageDataDefine.GAME_DIC + StageDataDefine.ICON_DIC + resourceName);
            if (imageResourceData == null)
            {
                Debug.LogError("【ロード失敗】" + StageDataDefine.IMAGE_DIC + StageDataDefine.GAME_DIC + StageDataDefine.ICON_DIC + resourceName);
                return;
            }
            imageResource = imageResourceData;
        }

        public override string ToString()
        {
            return string.Format("uniqueId={0}, iconName={1}, resourceName={2}", uniqueId, iconName, resourceName);
        }
    }
}
