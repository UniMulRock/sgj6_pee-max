using Utility.System;
using UnityEngine;
using System.Collections.Generic;

namespace PeeMax.Stage
{
	public class StageManager : SingletonMonoBehaviour<StageManager>
	{
		/// <summary>
		/// 開始位置
		/// </summary>
		[SerializeField]
		private StartData startPoint;

		/// <summary>
		/// 終了位置
		/// </summary>
		[SerializeField]
		private List<GoalData> goalPointList;
	}
}