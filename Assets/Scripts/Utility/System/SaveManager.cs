using UnityEngine;
using PeeMax.Stage;
using Utility.System.Data;
using Utility.System;

namespace Utility.System
{
	public class SaveManager : SingletonMonoBehaviour <SaveManager>
	{
        
		public void Save(SaveData data)
		{
			PlayerPrefs.SetInt(StageDataDefine.STAGE_ID_KEY, data.stageUniqueId);
			PlayerPrefs.Save();
		}

		public int Load()
		{
			return PlayerPrefs.GetInt(StageDataDefine.STAGE_ID_KEY, 0);
		}

        [ContextMenu("DeletePrefs")]
		public void DeletePrefs()
		{
			PlayerPrefs.DeleteAll();
		}
	}
}