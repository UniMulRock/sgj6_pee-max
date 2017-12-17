using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeeMax.Char
{
	public class CharMoveCtrl : CharController {

		public int MoveBlocks;

		float moveTime;
		bool isend = false;

		public override bool IsDone ()
		{
			return isend;
		}

		public override void Init (MonoBehaviour parent)
		{
			moveTime = 0.0f;
			isend = false;
		}

		public override void Do (MonoBehaviour parent)
		{
			if (moveTime <= (float)MoveBlocks) {
				moveTime += Time.deltaTime;

				// 前進
				parent.transform.Translate (new Vector3 (0, 0, CharDefine.MOVE_SPEED) * Time.deltaTime * CharDefine.BLOCK_LENGTH);
			} else {
				isend = true;
			}
		}

	}

}
