using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public enum AI_STATE{
    ATTACK,
    RETREAT
}
public class AIControllerScript : MonoBehaviour,IController
{
    public WeaponManager m_weaponManager;
    public WeaponAIBehaviorScript m_currentWeapon;
    public AI_STATE m_currentAIState;
    public Transform m_target;
    public ContactFilter2D m_scanLayers;
    public ContactFilter2D m_noneLayers;
    public float m_scanRadius;
    public float m_lrTime;
    public List<RaycastHit2D> m_scanList;
    public List<WeaponAIBehaviorScript> m_activeWeapon;
    public Collider2D[] m_scanWallList;
    private Coroutine m_lrC;
    public float m_reactionTime;
    public float m_aiStateReactionTime;
    public bool m_isReacting;
  
    
    public void Awake(){
        if (m_weaponManager == null) m_weaponManager = GetComponent<WeaponManager>(); 
        m_isReacting = false;
        
    }
    public void RemoveCD(CooldownObjectScript cd){
        m_activeWeapon.Remove(cd.GetComponent<WeaponAIBehaviorScript>());
    }
    public void AddCD(CooldownObjectScript cd){
        WeaponAIBehaviorScript wp = cd.GetComponent<WeaponAIBehaviorScript>();
        if (!m_activeWeapon.Contains(wp))m_activeWeapon.Add(wp);
    }
    void Update(){
        if (m_activeWeapon.Count == 0 || m_isReacting) return;
        Vector2 dis = transform.position - m_target.transform.position;
       
        float max = 0;
        WeaponAIBehaviorScript chosen = null;
        foreach (WeaponAIBehaviorScript wp in m_activeWeapon){
            float eval = wp.CurrentWorth(dis.magnitude);
            if (eval > max){
                max = eval;
                chosen = wp;
            }
        }
        if (m_currentWeapon == null) m_currentWeapon = chosen;
        StartCoroutine(ReactCoroutine());
        if (m_currentWeapon != chosen) {
            m_weaponManager.OnDoubleTap();
            m_currentWeapon = chosen;
            return;
        }
        Vector2 dir = m_currentWeapon.WeaponAIBehavior(m_target, m_currentAIState);
        m_weaponManager.OnStart(Vector2.zero);
        m_weaponManager.OnHold(dir);
        m_weaponManager.OnRelease(dir);
    }
    public IEnumerator ReactCoroutine(){
        m_isReacting = true;
        yield return new WaitForSeconds(m_reactionTime);
        m_isReacting = false;
    }
    public IEnumerator AIStateCoroutine(){
        float vab = Mathf.Max(0.65f,GetComponent<HealthComponent>().m_currentHealth / GetComponent<HealthComponent>().m_maxHealth);
        float ran = UnityEngine.Random.Range(0,vab);
        if (ran <= 0.25f) m_currentAIState = AI_STATE.RETREAT;
        else m_currentAIState = AI_STATE.ATTACK;
        yield return new WaitForSeconds(m_aiStateReactionTime);
        StartCoroutine(AIStateCoroutine());
    }
    public void SetControllerState(bool state){
        this.enabled = state;
        if (state == false) {
            m_activeWeapon.Clear();
            StopAllCoroutines();
        }
        else {
            StartCoroutine(AIStateCoroutine());
            foreach (WeaponScript wp in m_weaponManager.m_weapons) m_activeWeapon.Add(wp.GetComponent<WeaponAIBehaviorScript>());
            m_currentWeapon = m_weaponManager.GetCurrentWeapon().GetComponent<WeaponAIBehaviorScript>();
            m_isReacting = false;
        }
    }
}
