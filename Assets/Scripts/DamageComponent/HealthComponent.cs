using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public float m_maxHealth;
   
    public float m_currentHealth;
    public int m_stocks;
    public int m_currentStocks;
    public UnityEvent<float> m_healthChangeEvent;
    public UnityEvent<int> m_stockChangeEvent;
    public UnityEvent<HealthComponent> m_deathEvent;
    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StockInit(int stock){
        m_stocks = stock;
        m_currentStocks = m_stocks;
        ModifyStock(0);
    }
    public void ChangeHealth(float change){
        if (m_currentHealth <= 0) return;
        SetHealth(m_currentHealth + change);
    }
    public void SetHealth(float value){
        //Debug.Log("Health value is " + value);
        m_currentHealth = Mathf.Clamp(value, 0, m_maxHealth);
        m_healthChangeEvent.Invoke(m_currentHealth / m_maxHealth);
        if (value <= 0) {
            ModifyStock(-1);
            m_deathEvent.Invoke(this);
        }
        
    }
    public void ResetHealth(){
        SetHealth(m_maxHealth);
    }
    public float GetCurrentHealth(){
        return m_currentHealth;
    }
    public void ResetStock(){
        m_currentStocks = m_stocks;
    }
    public void ModifyStock(int mod){
        m_currentStocks += mod;
        m_currentStocks = Mathf.Clamp(m_currentStocks, 0, m_stocks);
        m_stockChangeEvent.Invoke(m_currentStocks);
    }
    public float GetCurrentStocks(){
        return m_currentStocks;
    } 
    
}
