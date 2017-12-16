using Utility.System;
using PeeMax.Stage;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class StageDataList : SingletonMonoBehaviour<StageDataList>
{
	[SerializeField]
	private List<StageData> iconDataList;

	protected override void Awake()
	{
		DontDestroyOnLoad(gameObject);

		// IconDataの取得
		using (var reader = new CSVReader<StageData>(StageDataDefine.CSV_DIC + StageDataDefine.ICON_DATA_CSV, true))
		{
			iconDataList = reader.ToList();
//			iconDataList.ForEach((data) =>
//				{
//					data.SetImageResource();
//				});
		}
	}


}
