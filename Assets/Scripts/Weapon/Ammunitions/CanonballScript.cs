using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballScript : BulletScript
{
    public LayerMask m_spawnLayer;
    private float m_healthLostPerSecond;
    private HealthComponent m_healthComponent;
    private SpriteRenderer m_spriteRenderer;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StopAllCoroutines();
        m_healthComponent = GetComponent<HealthComponent>();
        m_healthLostPerSecond = m_healthComponent.m_maxHealth / m_lifeTime;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        //gameObject.layer = 
    }

    // Update is called once per frame
    void Update()
    {
        m_healthComponent.ChangeHealth(-m_healthLostPerSecond * Time.deltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D coll){
    }
    public void SetDeath(HealthComponent healthComponent){
        Explode();
        //Debug.Log("died");
    }
    public void SetFillState(float ratio){
        m_spriteRenderer.size = new Vector2(ratio,ratio);
    }
    
}
