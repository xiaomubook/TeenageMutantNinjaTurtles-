using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMNT
{
    public class HandleAnimBooleans : StateMachineBehaviour
    {
        public BoolHolder[] boolHolders;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < boolHolders.Length; i++)
            {
                animator.SetBool(boolHolders[i].boolName, boolHolders[i].status);
            }
        }


        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < boolHolders.Length; i++)
            {
                if(boolHolders[i].restOnExit)
                    animator.SetBool(boolHolders[i].boolName, !boolHolders[i].status);
            }
        }
        [System.Serializable]
        public class BoolHolder
        {
            public string boolName;
            public bool status;
            public bool restOnExit;
        }
    }
}
