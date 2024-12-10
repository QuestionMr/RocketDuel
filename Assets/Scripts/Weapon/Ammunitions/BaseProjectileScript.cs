using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectileScript : MonoBehaviour
{
    public float m_travelSpeed;
    public float m_lifeTime;
    protected Vector3 m_velocity;
    public BulletBehavior[] m_bulletBehaviors;
    protected virtual void Start()
    {
        StartCoroutine(LifeTimeTracking());
    }
    public virtual void SetTrailColor(Color c){
        GetComponent<SpriteRenderer>().color = c;
        if (TryGetComponent<TrailRenderer>(out TrailRenderer tr)){
            tr.startColor = c;
            c.a = 0.2f;
            tr.endColor = c;
        }
    }
     public virtual void SetMovement(Vector2 dir){
        m_velocity = dir * m_travelSpeed;
        SetRotation(dir);
        GetComponent<Rigidbody2D>().velocity = m_velocity;
    }
    public virtual void SetRotation(Vector2 dir){
        transform.right = dir;
    }
    protected virtual IEnumerator LifeTimeTracking(){
        yield return new WaitForSeconds(m_lifeTime);
        LifeTimeAction();
    }
    protected virtual void LifeTimeAction(){

    }
    public virtual void SetExcludeLayers(LayerMask TexcludeLayers){
        GetComponent<Collider2D>().excludeLayers = TexcludeLayers; 
    }
}
