using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageableScript : MonoBehaviour, IDamageable
{
    public List<FilterDataSO> m_filterData;
    public float m_currentMaxDamageReceive;
    public float m_maxDamageReceive;

    void Awake(){
        m_currentMaxDamageReceive = m_maxDamageReceive;
    }
    public void DamageBehaviour(DamageData damageData)
    {
        if (PassDamageFilter(damageData)) {
            TakeDamage(damageData);
        }
    }

    public virtual bool PassDamageFilter(DamageData damageData){
        foreach(FilterDataSO filterData in m_filterData) {
            if (!filterData.FilterCheck(gameObject, damageData)) return false;
        }
        return true;
    }

    public virtual void TakeDamage(DamageData damageData){
        Debug.Log("HITITITIT");
        float damage = damageData.m_damageAmount;
        damage = Mathf.Max(damage, -m_currentMaxDamageReceive);

        GetComponent<HealthComponent>().ChangeHealth(damage);
        if (damage < 0) {
            EffectManager.m_instance.PlayHitEffect((-damage).ToString(), transform.position);
            AudioManager.m_instance.PlayHitSound();
        }
    }

    public void SetMaxDamageCap(float cap){
        m_currentMaxDamageReceive = cap;
    }
}
