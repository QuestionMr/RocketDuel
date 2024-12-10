using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CooldownObjectScript : MonoBehaviour
{
    public float m_baseCooldown;
    private float m_currentCooldown;
    public bool m_isOnCooldown;
    public bool m_isCooldownPaused = false;
    public UnityEvent<CooldownObjectScript> m_cooldownStartEvent;
    public UnityEvent<CooldownObjectScript> m_cooldownEndEvent;

    public bool IsOnCooldown(){
        return m_currentCooldown > 0;
    }
    public void SetCooldownState(bool state){
        SetCurrentCooldown(state? m_baseCooldown:0);
        if (state) m_cooldownStartEvent.Invoke(this);
        else m_cooldownEndEvent.Invoke(this);
    }
    public void SetCurrentCooldown(float cd){
        m_currentCooldown = cd;
        if (cd <= 0) m_isOnCooldown = false;
        else m_isOnCooldown = true;
    }
    public void UpdateCooldown(){
        if (m_isCooldownPaused) return;
        m_currentCooldown -= Time.deltaTime; 
    }
    public float CooldownRatio(){
        return (m_baseCooldown - m_currentCooldown) / m_baseCooldown;
    }
    public float GetCurrentCooldown(){
        return m_currentCooldown;
    }
}
