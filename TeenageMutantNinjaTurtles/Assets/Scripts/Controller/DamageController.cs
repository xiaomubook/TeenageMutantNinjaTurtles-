using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMNT
{
    public class DamageController : MonoBehaviour
    {
        PlayerController owner;

        private void Start()
        {
            owner = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController u = other.GetComponentInParent<PlayerController>();
            if (u != null)
            {
                if (u != owner)
                {
                    if (u.team != owner.team || owner.GetLastAction.canHitAllies)
                    {
                        u.OnHit(owner.GetLastAction, owner.isLookingLeft);
                    }

                }
            }
        }
    }
}
