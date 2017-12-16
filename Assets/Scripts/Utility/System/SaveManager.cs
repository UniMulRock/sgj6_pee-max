using UnityEngine;
using PeeMax.Stage;
using Utility.System.Data;
using Utility.System;

namespace Utility.System
{
	public class SaveManager : SingletonMonoBehaviour <SaveManager> 
	{
		public void SaveStageId(SaveData data)
		{
			PlayerPrefs.SetInt(StageDataDefine.STAGE_ID_KEY, data);
			PlayerPrefs.Save();
		}

		public int LoadStageId()
		{
			return PlayerPrefs.GetInt (StageDataDefine.STAGE_ID_KEY, 0);
		}

		public void ClearPrefs ()
		{
			PlayerPrefs.DeleteAll ();
		}
	}
}