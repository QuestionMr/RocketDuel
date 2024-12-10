
using UnityEngine;

public class PillScript : BulletScript
{
    private bool m_rebounded;
    public Vector2 m_offSetLaunch;
    protected override void Start()
    {
        base.Start();
        m_rebounded = false;
    }
    protected override void OnTriggerEnter2D(Collider2D coll){
        return;
    }
    public override void SetMovement(Vector2 dir){
        base.SetMovement(dir + m_offSetLaunch);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-8f,8f));
    }
    public void OnCollisionEnter2D(Collision2D coll){
        // Debug.Log("colled");
        // Debug.Log(coll.collider.gameObject.layer +  " " + m_blockingLayers.value + " "
        // + (1 << coll.collider.gameObject.layer));
        if (((1 << coll.collider.gameObject.layer) & m_blockingLayers.value) == 0) Explode(coll.GetContact(0).point);
        else if (!m_rebounded){
            m_rebounded = true;
            m_explosionDamageScale *= 0.5f;
        }
    }

}
