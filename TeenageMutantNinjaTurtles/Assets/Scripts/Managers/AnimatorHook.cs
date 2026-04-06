using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMNT
{
    public class AnimatorHook : MonoBehaviour
    {
        Animator anim;
        public Vector3 deltaPosition;
        public bool IsInteracting
        {
            get { return anim.GetBool("isInteracting"); }
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
        }
        public void PlayAnimation(string animName)
        {
            anim.Play(animName);
            anim.SetBool("isInteracting", true);
        }

        public void Tick(bool isMoving)
        {
            float v = isMoving ? 1 : 0;
            anim.SetFloat("move", v);
        }

        private void OnAnimatorMove()
        {
            deltaPosition = anim.deltaPosition / Time.deltaTime;
        }
    }
}
