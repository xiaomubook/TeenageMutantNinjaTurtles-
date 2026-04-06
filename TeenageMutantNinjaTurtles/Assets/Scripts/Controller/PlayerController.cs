using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TMNT
{
    public class PlayerController : MonoBehaviour
    {
        AnimatorHook animatorHook;
        [HideInInspector]
        public NavMeshAgent agent;
        public Transform holder;
        public int health = 100;
        public int team;
        public bool isLookingLeft;
        public float horizontalSpeed = 0.8f;
        public float verticalSpeed = 0.6f;
        public ActionData[] actions;
        public bool isAI;
        public bool hasBackHit;

        public bool IsInteracting
        {
            get { return animatorHook.IsInteracting; }
        }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animatorHook = GetComponentInChildren<AnimatorHook>();

            agent.updateRotation = false;
        }

        public void TickPlayer(float delta,Vector3 direction)
        {
            direction.x *= horizontalSpeed;
            direction.z *= verticalSpeed;

            bool isMoving = direction.sqrMagnitude > 0;

            

            agent.velocity = direction;// * delta;
            animatorHook.Tick(isMoving);
     
        }

        public void UseRootMotion()
        {
            agent.velocity = animatorHook.deltaPosition;
        }

        public void HandRotation(bool looksLeft)
        {
            Vector3 eulers = Vector3.zero;
            isLookingLeft = false;
            if (looksLeft)
            {
                eulers.z = 180;
                isLookingLeft = true;
            }

            holder.localEulerAngles = eulers;
        }

        ActionData storedAction;
        public ActionData GetLastAction
        {
            get
            {
                return storedAction;
            }
        }

        public void PlayAction(ActionData actionData)
        {
            PlayAnimation(actionData.actionAnim);
            storedAction = actionData;
        }

        public void PlayAnimation(string animName)
        {
            animatorHook.PlayAnimation(animName);
        }

        public void OnHit(ActionData actionData, bool hitterLooksLeft)
        {
            bool isFromBehind = false;

            if (isLookingLeft && hitterLooksLeft
                || !hitterLooksLeft && !isLookingLeft)
            {
                isFromBehind = true;
            }

            if (!hasBackHit)
            {
                if (isFromBehind)
                {
                    HandRotation(!hitterLooksLeft);
                }
                isFromBehind = false;
            }

            switch (actionData.damageType)
            {
                case DamageType.light:
                    if (isFromBehind)
                    {
                        PlayAnimation("hit_light_back");
                    }
                    else
                    {
                        PlayAnimation("hit_light_front");
                    }
                    
                    break;
                case DamageType.mid:
                    if (isFromBehind)
                    {
                        PlayAnimation("hit_mid_back");
                    }
                    else
                    {
                        PlayAnimation("hit_mid_front");
                    }
                    
                    break;
                case DamageType.heavy:
                    if (isFromBehind)
                    {
                        PlayAnimation("knockdown_back");
                    }
                    else
                    {
                        PlayAnimation("knockdown_front");
                    }
                    
                    break;
                default:

                    break;
            }
        }

    }
}
