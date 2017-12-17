using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeeMax.Char
{
	public class CharTurnRightCtrl : CharController {

		public override bool IsDone ()
		{
			return false;
		}

		public override void Init (MonoBehaviour parent)
		{
		}

		public override void Do (MonoBehaviour parent)
		{
			// 右
			parent.transform.Rotate(new Vector3(0,CharDefine.TURN_RIGHT,0));
		}
	}

}

