using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWP : WeaponScript
{
    public ContactFilter2D m_affectedLayers;
    public ContactFilter2D m_blockingLayers;
    protected List<Collider2D> m_overlapResults;
    private List<RaycastHit2D> m_rayhitResults;
    public Transform m_effectSpawnPos;

    public float m_meleeRadius;
    public float m_angleOffset;

    public float m_meleeDamage;
    public float m_meleeKnockback;
    public float m_meleeDashForce;
    public GameObject m_slashEffectPrefab;

    protected override void StartProcedure(){
        m_overlapResults = new List<Collider2D>();
        m_rayhitResults = new List<RaycastHit2D>();
    }
    public override void ReleaseAction(Vector2 diff)
    {
        base.ReleaseAction(diff);
        EffectManager.m_instance.PlayCustomEffect(m_slashEffectPrefab,transform.position,transform.rotation);
        m_overlapResults.Clear();
        CustomCast.FanCast(transform.position, m_meleeRadius, diff, m_angleOffset, m_affectedLayers, m_blockingLayers, m_rayhitResults, 10, 5);
        foreach (RaycastHit2D hit in m_rayhitResults){
            if (!m_overlapResults.Contains(hit.collider) && hit.collider.gameObject != m_owningPlayer) m_overlapResults.Add(hit.collider);
        }
        
        TargetAction(diff);
        

    }
    public virtual void TargetAction(Vector2 diff){
        DamageData damageData = new DamageData(GetComponent<TeamEntityScript>().m_teamId,-m_meleeDamage);
        int hitInvert = 1;
        foreach (Collider2D hitCollider in m_overlapResults){
            Debug.Log(hitCollider.name + "yeah");
            damageData.m_damageStartingPos = hitCollider.transform.position;
            if (hitCollider.TryGetComponent<IDamageable>(out IDamageable idamageable)) idamageable.DamageBehaviour(damageData);
            if (hitCollider.GetComponent<Rigidbody2D>() != null){
                hitCollider.GetComponent<Rigidbody2D>().AddForce((hitCollider.transform.position - transform.position + (Vector3)diff).normalized * m_meleeKnockback);
                hitInvert = -1;
            }
        }
        //GetComponentInParent<Rigidbody2D>().AddForce(diff.normalized * m_meleeDashForce * hitInvert, ForceMode2D.Force);
        GetComponentInParent<Rigidbody2D>().velocity = diff.normalized * m_meleeDashForce * hitInvert;
    }
}
