using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeeMax.Char
{
	public class CharTurnLeftCtrl : CharController {

		float moveTime;
		bool isend = false;

		public override bool IsDone ()
		{
			return isend;
		}

		public override void Init (GameObject parent)
		{
		}

		public override void Do (GameObject parent)
		{
			if (moveTime <= 1.0f) {
				moveTime += Time.deltaTime;

				// 左
				parent.transform.Rotate (new Vector3 (0, CharDefine.TURN_LEFT, 0) * Time.deltaTime);
			} else {
				isend = true;
			}
		}

		public override string ToString(){
			return "CharTurnLeftCtrl";
		}
	}
}
