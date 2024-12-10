using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMaceScript : MonoBehaviour
{
    public float m_damageMultiplier;
    private TeamEntityScript m_teamEntityScript;
    public float m_minDamageSpeed;
    public float m_maxDamageSpeed;

    [Range(0,1)]
    public float m_minDamageScale;

    [Range(0,1)]
    public float m_maxDamageScale;
    public float m_reboundfForceMultiplier;

    [Range(0,1)]
    public float m_minReboundScale;

    [Range(0,1)]
    public float m_maxReboundScale;
    private Rigidbody2D m_rb;
    private float m_prevFrameSpeed;
    private Vector2 m_prevFrameVelocity;
    public Color[] m_currentStateColor;
    //private int m_currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(GameConfigSO.m_hazard);
        m_rb = GetComponent<Rigidbody2D>();
        m_teamEntityScript = GetComponent<TeamEntityScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Frame " + m_currentFrame++);
    }
    void FixedUpdate()
    {
        m_prevFrameSpeed = m_rb.velocity.magnitude;
        m_prevFrameVelocity = m_rb.velocity;
        
    }
    void OnCollisionEnter2D(Collision2D coll){
        if (m_teamEntityScript.m_teamId == 0) return;
        if (coll.collider.TryGetComponent<TeamEntityScript>(out TeamEntityScript teamEntityScript) 
        && teamEntityScript.m_teamId != m_teamEntityScript.m_teamId)
        {
            //Debug.Log("Colldd");
            Vector3 velDiff = coll.rigidbody.velocity - m_prevFrameVelocity;
            if( velDiff.magnitude < m_minDamageSpeed) return;
            if (coll.collider.TryGetComponent<HealthComponent>(out HealthComponent health)){

                float speedRatio = velDiff.magnitude / m_maxDamageSpeed;
                health.ChangeHealth(-Mathf.Ceil(Mathf.Clamp(speedRatio * speedRatio * m_rb.mass, m_minDamageScale, m_maxDamageScale) * m_damageMultiplier));

                float reboundForceMagnitude = Mathf.Clamp(speedRatio, m_minReboundScale, m_maxReboundScale) * m_reboundfForceMultiplier;
                Vector3 reboundForce = (coll.transform.position - transform.position).normalized * reboundForceMagnitude;
                coll.rigidbody.AddForce(reboundForce);
                //m_rb.totalForce = Vector2.zero;
                //m_rb.velocity = -m_rb.velocity;
                m_rb.AddForce(-reboundForce);
                SetCurrentDamageId(0);
            }
            return;
        }
        if (m_prevFrameSpeed < m_minDamageSpeed) SetCurrentDamageId(0); 
    }
    // void OnTriggerEnter2D(Collider2D coll){
    //     if (coll.TryGetComponent<TeamEntityScript>(out TeamEntityScript teamEntityScript)){
    //        SetCurrentDamageId(teamEntityScript.m_teamId);
    //     }
    // }
    public void SetCurrentDamageId(int id){
        if (m_teamEntityScript.m_teamId == id) return;
        m_teamEntityScript.m_teamId = id;
        GetComponent<SpriteRenderer>().color = m_currentStateColor[m_teamEntityScript.m_teamId];
    }
}
