using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviourSript : WeaponAIBehaviorScript
{
     public override Vector2 WeaponAIBehavior(Transform target, AI_STATE state){
    
        Vector2 dir = target.position - transform.position;
        return dir.normalized * 100 * ((state == AI_STATE.ATTACK)?1:-1);
    }
}
