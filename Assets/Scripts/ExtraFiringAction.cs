using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraFiringAction : ExtraActionBridge
{
    private WeaponScript m_weapon;
    public int m_extraFiringAmount;
    public float m_fireRate;

    [Range(-45,45)]
    public float m_randomFireRange;
    private Coroutine m_extraFiringCoroutine;
    [Range(0,180)]
    public float m_spread;
    public override void ExtraBehavior(Vector2 dir)
    {
       if (m_extraFiringCoroutine != null) return;
       m_extraFiringCoroutine = StartCoroutine(ExtraFiringCoroutine(dir));
    }

    void Awake()
    {
        m_weapon = GetComponent<WeaponScript>();
    }

    public IEnumerator ExtraFiringCoroutine(Vector2 dir){
        for (int i = 0; i < m_extraFiringAmount; i++){
            Vector2 newDir = Quaternion.AngleAxis(Random.Range(-m_spread, m_spread), Vector3.forward) * dir;
            
            yield return new WaitForSeconds(m_fireRate);
            m_weapon.ReleaseAction(newDir);
        }
        m_extraFiringCoroutine = null;
    }
}
