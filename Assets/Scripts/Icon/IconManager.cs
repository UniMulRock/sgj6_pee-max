using Utility.System;
using PeeMax.Stage;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace PeeMax.Icon
{
    public class IconManager : SingletonMonoBehaviour<IconManager>
    {
        [SerializeField]
        private List<IconData> iconDataList;

        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);

            // IconDataの取得
            using (var reader = new CSVReader<IconData>(StageDataDefine.CSV_DIC + StageDataDefine.ICON_DATA_CSV, true))
            {
                iconDataList = reader.ToList();
                iconDataList.ForEach((data) =>
                    {
                        data.SetImageResource();
                    });
            }
        }
    }
}