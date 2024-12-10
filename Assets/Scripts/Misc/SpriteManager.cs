using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    private Color m_originalColor;
    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_originalColor = m_spriteRenderer.color;
    }

    void Update()
    {
        
    }
    public void SetColor(Color col){
        m_spriteRenderer.color = col;
    }
    public void SetDeathColor(){
        m_spriteRenderer.color = Color.grey;
    }
    public void SetSpawnColor(){
        m_spriteRenderer.color = Color.white;
    }
    public void ResetColor(){
        if (m_spriteRenderer == null){
            Debug.Log("IS NULL");
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }
        m_spriteRenderer.color = m_originalColor;
    }
}
