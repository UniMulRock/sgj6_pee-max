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

		public abstract void Init (GameObject parent);

		public abstract void Do (GameObject parent);
        
    }
}