using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeeMax.Char
{
	public abstract class CharController : MonoBehaviour
    {
        void Awake()
        {
            //Start Condition   
        }

        void Update()
        {
			
		}

		public abstract bool IsDone ();

		public abstract void Init (MonoBehaviour parent);

		public abstract void Do (MonoBehaviour parent);
        
    }
}