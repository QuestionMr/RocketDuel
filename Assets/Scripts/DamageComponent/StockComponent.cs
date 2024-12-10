using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StockComponent : MonoBehaviour
{
    public int m_stocks;
    public int m_currentStocks;
    public UnityEvent<StockComponent> m_deathEvent;
    //public UnityEvent<int> m_lostStockEvent;
    void Start()
    {
        m_currentStocks = m_stocks;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetStock(){
        m_currentStocks = m_stocks;
    }
    public void CheckStockDeath(float health){
        if (health <= 0) {
            if (m_currentStocks == 0) return;
            ModifyStock(-1);
            m_deathEvent.Invoke(this);
        }
    
    }
    public void ModifyStock(int mod){
        m_currentStocks += mod;
        m_currentStocks = Mathf.Clamp(m_currentStocks, 0, m_stocks);
    }
    public float GetCurrentStocks(){
        return m_currentStocks;
    } 
}
