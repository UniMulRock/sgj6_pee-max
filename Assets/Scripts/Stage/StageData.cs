using System;
using UnityEngine;
using System.Collections.Generic;

namespace PeeMax.Stage
{
	[Serializable]
	public class StageData
	{
		[CsvColumnAttribute(0)]
		[SerializeField]
		private int uniqueId;

		[CsvColumnAttribute(1)]
		[SerializeField]
		private string stageName;

		[CsvColumnAttribute(2)]
		[SerializeField]
		private string stageDataName;

		[CsvColumnAttribute(3)]
		[SerializeField]
		private int iconUniqueId01;

		[CsvColumnAttribute(4)]
		[SerializeField]
		private int iconUniqueId02;

		[CsvColumnAttribute(5)]
		[SerializeField]
		private int iconUniqueId03;

		public int[] IconUniqueIdArray{ get{ return new int[]{iconUniqueId01,iconUniqueId02,iconUniqueId03}; } }

		public string StageName{ get { return stageName; } }

		public string StageDataName{ get { return stageDataName; } }

		public List<Sprite> IconList{ get; set; }

		public override string ToString()
		{
			return string.Format("uniqueId={0}, stageName={1}, stageDataName={2}", uniqueId, stageName, stageDataName);
		}
	}
}