using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorMeleeWP : MeleeWP
{
    public override void TargetAction(Vector2 diff)
    {
        //DamageData damageData = new DamageData(GetComponent<TeamEntityScript>().m_teamId,-m_meleeDamage);
        foreach (Collider2D hitCollider in m_overlapResults){
            Debug.Log(hitCollider.name + "yeah");
            hitCollider.GetComponent<Rigidbody2D>().velocity = hitCollider.GetComponent<Rigidbody2D>().velocity.magnitude * diff.normalized;
            //damageData.m_damageStartingPos = hitCollider.transform.position;
            // if (hitCollider.TryGetComponent<IDamageable>(out IDamageable idamageable)) idamageable.DamageBehaviour(damageData);
            // if (hitCollider.GetComponent<Rigidbody2D>() != null){
            //     hitCollider.GetComponent<Rigidbody2D>().AddForce((hitCollider.transform.position - transform.position + (Vector3)diff).normalized * m_meleeKnockback);
            // }
        }
        //GetComponentInParent<Rigidbody2D>().AddForce(diff.normalized * m_meleeDashForce * hitInvert, ForceMode2D.Force);
        GetComponentInParent<Rigidbody2D>().velocity = diff.normalized * m_meleeDashForce;

    }
}
