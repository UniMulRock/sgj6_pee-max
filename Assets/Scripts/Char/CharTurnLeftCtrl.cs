using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeeMax.Char
{
	public class CharTurnLeftCtrl : CharController {

		public override bool IsDone ()
		{
			return false;
		}

		public override void Init (MonoBehaviour parent)
		{
		}

		public override void Do (MonoBehaviour parent)
		{
			// 左
			parent.transform.Rotate(new Vector3(0,CharDefine.TURN_LEFT,0));
		}

	}
}
