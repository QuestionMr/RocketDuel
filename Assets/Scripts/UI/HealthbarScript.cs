using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthComponent m_healthComponent;
    private Transform m_targetTransform;
    public Image m_bar;
    public Image m_bgBar;
    void Awake()
    {
        m_targetTransform = m_healthComponent.transform;
        m_healthComponent.m_healthChangeEvent.AddListener(UpdateHealthbar);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_targetTransform.position;
        
    }

    private void UpdateHealthbar(float ratio){
        Debug.Log("Bar ratio is " + ratio);
        m_bar.fillAmount = ratio;
    }
    public void SetHealthBarVisibility(bool isVisible){
        m_bar.enabled = isVisible;
        m_bgBar.enabled = isVisible;
    }
}
