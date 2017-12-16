using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeeMax.Char
{
    public class CharController : MonoBehaviour, CharControllerInterface
    {
        void Awake()
        {
            //Start Condition   
        }

        void Update()
        {
            Do();
            if (IsDone() == true)
            {
                // Next command
            }
        }

        public virtual bool IsDone(){
            return true;
        }

        public virtual void Do(){}

        [ContextMenu("Move")]
        public virtual void Move()
        {
            // 前進
            transform.Translate(new Vector3(0,0,CharDefine.MOVE_SPEED));
        }

        [ContextMenu("TurnLeft")]
        public virtual void TurnLeft()
        {
            // 左
            transform.Rotate(new Vector3(0,CharDefine.TURN_LEFT,0));
        }

        [ContextMenu("TurnRight")]
        public virtual void TurnRight()
        {
            // 右
            transform.Rotate(new Vector3(0,CharDefine.TURN_RIGHT,0));
        }
        
    }
}