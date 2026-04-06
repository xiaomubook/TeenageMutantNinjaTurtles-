using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMNT
{
    [System.Serializable]
    public class ActionData 
    {
        public string actionAnim;
        public DamageType damageType;
        public bool canHitAllies;
    }
    [System.Serializable]
    public enum DamageType
    {
        light,
        mid,
        heavy
    }
}
