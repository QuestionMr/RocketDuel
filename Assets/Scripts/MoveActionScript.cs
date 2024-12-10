using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionScript : ExtraActionBridge
{
    private Rigidbody2D m_rb;
    public float m_moveForce;
    public Vector2 m_lastFrameVector;
    public float m_totalMaxSpeed;
    public override void ExtraBehavior(Vector2 dir)
    {
        dir.Normalize();
        dir.y = 0;
        m_lastFrameVector = dir * m_moveForce * Time.deltaTime;

        if (m_rb.velocity.x * m_lastFrameVector.x >= 0){
            if (m_rb.velocity.magnitude >= m_totalMaxSpeed) return;
            float t = Mathf.Min(m_lastFrameVector.x, m_totalMaxSpeed - m_rb.velocity.x);
            m_lastFrameVector.x = t;
        }
        m_rb.velocity += m_lastFrameVector;
        


    }
    // public void OnHoldBehavior(GameObject player, Vector2 dir)
    // {
    //     player.GetComponent<Rigidbody2D>().velocity = dir;

    // }
    void Awake()
    {
        m_rb = transform.parent.GetComponent<Rigidbody2D>();
    }

}
