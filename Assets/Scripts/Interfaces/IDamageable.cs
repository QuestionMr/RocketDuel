using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DamageData{
    public DamageData(int teamId, float amount){
        m_damageTeamId = teamId;
        m_damageAmount = amount;
    }
    public DamageData(int teamId, float amount, Vector3 damageStartingPos){
        m_damageTeamId = teamId;
        m_damageAmount = amount;
        m_damageStartingPos = damageStartingPos;
    }
    public int m_damageTeamId;
    public float m_damageAmount;
    public Vector3 m_damageStartingPos;
}
public interface IDamageable
{
    public void TakeDamage(DamageData damageData);
    public void DamageBehaviour(DamageData damageData);
    public bool PassDamageFilter(DamageData damageData);
}
