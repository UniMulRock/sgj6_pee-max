using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Animation
{
    public class AnimationEvent : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public Action AnimationEndEvent;

        public Animator GetAnimator()
        {
            return animator;
        }

        public void AnimationEnd()
        {
            if (AnimationEndEvent != null)
            {
                AnimationEndEvent();
            }
        }
    }
}