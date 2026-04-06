using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMNT
{
    public class InputHandler : MonoBehaviour
    {
        public PlayerController playerController;
        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 targetDirection = Vector3.zero;

            targetDirection.x = h;
            targetDirection.z = v;
            
            if (playerController.IsInteracting)
            {
                playerController.UseRootMotion();
            }
            else
            {
                if (targetDirection.x != 0)
                {
                    playerController.HandRotation(targetDirection.x < 0);
                }
                
                playerController.TickPlayer(Time.deltaTime, targetDirection.normalized);

                if (Input.GetButton("Fire1"))
                {
                    playerController.PlayAction(playerController.actions[0]);
                }
            }                          
        }
    }
}
