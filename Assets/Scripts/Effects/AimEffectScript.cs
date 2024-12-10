using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEffectScript : MonoBehaviour
{
    private SpriteRenderer m_childRenderer;
    protected virtual void Awake()
    {
        m_childRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        m_childRenderer.enabled = false;
    }
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetEffectVisibility(bool enabled){
        if (m_childRenderer.enabled == enabled) return;
        m_childRenderer.enabled = enabled;
    } 
    public virtual void SetEffectDirection(Vector3 dir){
        //transform.right = dir;
    }
}
