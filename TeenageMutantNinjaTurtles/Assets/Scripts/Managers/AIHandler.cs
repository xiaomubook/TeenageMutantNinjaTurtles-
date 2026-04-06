using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMNT
{
    public class AIHandler : MonoBehaviour
    {
        float attackTime = 0.3f;
        float attackRate = 1.5f;
        public PlayerController playerController;
        public Transform target;
        public float attackDistance = 2f;

        public bool IsInteracting
        {
            get
            {
                return playerController.IsInteracting;
            }
        }

        private void Start()
        {
            playerController.isAI = true;
        }

        private void Update()
        {
            if (target == null)
                return;            

            if (IsInteracting)
            {
                playerController.UseRootMotion();
                return;
            }

            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.Normalize();
            directionToTarget.z = 0;

            Vector3 targetPosition = target.position + (directionToTarget * -1) * attackDistance;

            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance > attackDistance)
            {
                playerController.agent.isStopped = false;
                playerController.agent.SetDestination(targetPosition);

                playerController.HandRotation(playerController.agent.velocity.x < 0);
            }
            else
            {
                playerController.agent.isStopped = true;
                playerController.HandRotation(directionToTarget.x < 0);

                if (attackTime > 0)
                {
                    attackTime -= Time.deltaTime;
                }
                else
                {
                    playerController.PlayAction(playerController.actions[0]);
                    attackTime = attackRate;
                }
            }

            playerController.TickPlayer(Time.deltaTime, playerController.agent.desiredVelocity);
        }
    }
}
