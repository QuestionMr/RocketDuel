
using System.Collections;
using UnityEngine;

public class PillV2 : BulletScript
{
    private bool m_rebounded;
    public Vector2 m_offSetLaunch;
    public float m_postWallExplosionScale;
    public float m_postWallExplosionForce;
    public int m_explodeNumber;
    protected override void Start()
    {
        base.Start();
        m_rebounded = false;
    }
    protected override void OnTriggerEnter2D(Collider2D coll){
        return;
    }
    public override void SetMovement(Vector2 dir){
        base.SetMovement(dir.normalized + m_offSetLaunch);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-8f,8f));
    }
    public void OnCollisionEnter2D(Collision2D coll){
        // Debug.Log("colled");
        // Debug.Log(coll.collider.gameObject.layer +  " " + m_blockingLayers.value + " "
        // + (1 << coll.collider.gameObject.layer));
        if (m_isFlaggedToDestroy) return;
        Vector2 temp = coll.GetContact(0).point;
            
        //Debug.Log("separ " + coll.GetContact(0).separation);
        if (((1 << coll.collider.gameObject.layer) & m_blockingLayers.value) != 0 && m_explodeNumber > 0) {
            m_explodeNumber--;
            if (coll.collider.OverlapPoint(temp)) {
                temp = coll.collider.ClosestPoint(temp + coll.GetContact(0).normal) + coll.GetContact(0).normal * 0.01f;
            }
            Explode(temp);
            m_explosionDamageScale = m_postWallExplosionScale;
            m_explosionForceScale = m_postWallExplosionForce;
        }
        if (((1 << coll.collider.gameObject.layer) & m_blockingLayers.value) == 0){
            Explode(temp);
            Destroy(gameObject);
        }
    }
    protected override void PostExplosionEvent()
    {
        
    }
     protected override IEnumerator LifeTimeTracking(){
        if (m_lifeTime >= 0){
            yield return new WaitForSeconds(m_lifeTime);
            Explode();
            Destroy(gameObject);
            m_isFlaggedToDestroy = true;
        }
    }

}
