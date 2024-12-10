using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMaceDamageScript : BaseDamageableScript
{
    public override void TakeDamage(DamageData damageData)
    {
        if (damageData.m_damageStartingPos == null) return;
        if ((GetComponent<Collider2D>().ClosestPoint(damageData.m_damageStartingPos) - (Vector2)damageData.m_damageStartingPos).magnitude <= 0.1f){
            GetComponent<PhysicsMaceScript>().SetCurrentDamageId(damageData.m_damageTeamId);
        }
    }
}
