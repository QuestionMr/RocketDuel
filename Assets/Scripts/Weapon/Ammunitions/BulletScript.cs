using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_travelSpeed;
    public float m_lifeTime;
    public BulletBehavior[] m_bulletBehaviors;
    public ContactFilter2D m_affectedLayers;
    public LayerMask m_blockingLayers;
    public float m_explosionRadius;
    public float m_explosionForceScale;
    public float m_explosionDamageScale;

    [Range(0.0f, 1.0f)]
    public float m_maxBaseExplosionDamage;
    
    [Range(0.0f, 1.0f)]
    public float m_maxBaseExlosionForce;
    [Range(0.0f, 1.0f)]
    public float m_minBaseExlosionForce;
    public int m_damageSlots;
    protected bool m_isFlaggedToDestroy;
    private List<Collider2D> m_overlapResults;
    private List<RaycastHit2D> m_raycastResults;
    public GameObject m_owningWeapon;


    protected Vector3 m_velocity;
    private Vector3 m_preCollPos;
    private Vector3 m_ogPos;
    private Vector3 m_originPos;
    private Vector3 m_explodeDir;
    public AudioClip m_explosionSound;
    
    protected virtual void Start()
    {
        StartCoroutine(LifeTimeTracking());
        m_isFlaggedToDestroy = false;
        m_overlapResults = new List<Collider2D>();
        m_raycastResults = new List<RaycastHit2D>();
        m_ogPos = transform.position;
        m_explodeDir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void SetTrailColor(Color c){
        GetComponent<SpriteRenderer>().color = c;
        if (TryGetComponent<TrailRenderer>(out TrailRenderer tr)){
            tr.startColor = c;
            c.a = 0.2f;
            tr.endColor = c;
        }
    }
    protected virtual void FixedUpdate(){
        //m_prePreCollPos = m_preCollPos;
        m_preCollPos = transform.position;
        //transform.position += m_velocity * Time.fixedDeltaTime;
    }
    public virtual void SetMovement(Vector2 dir){
        m_velocity = dir * m_travelSpeed;
        m_originPos = transform.position;
        transform.position += GetComponent<Collider2D>().bounds.size.x * 0.55f * (Vector3)dir.normalized;
        SetRotation(dir);
        GetComponent<Rigidbody2D>().velocity = m_velocity;
    }
    public virtual void SetRotation(Vector2 dir){
        transform.right = dir;
    }
    
    // void OnCollisionEnter2D(Collision2D coll){
    //     if (m_isFlaggedToDestroy) return;
    //     Explode(coll.collider);
    //     Destroy(gameObject);
    //     m_isFlaggedToDestroy = true;
    // }
    protected virtual void OnTriggerEnter2D(Collider2D coll){
        if (m_isFlaggedToDestroy) return;
        Explode(coll);
        
    }
    protected virtual Vector3 FindExplodePos(Collider2D explodeSurface){
        //Find the collide position of the bullet against the surface;

        Vector3 explodePos;
        RaycastHit2D hitS;
        if (explodeSurface != null) {
            if (explodeSurface.OverlapPoint(m_preCollPos)){
                hitS= Physics2D.Raycast(m_originPos, m_velocity, Mathf.Infinity, 1 << explodeSurface.gameObject.layer);
            }
            else {
                hitS = Physics2D.CircleCast(m_preCollPos,gameObject.GetComponent<Collider2D>().bounds.size.x * 0.5f, 
                                            m_velocity, Mathf.Infinity, 1 << explodeSurface.gameObject.layer);
                if (hitS.collider == null || explodeSurface.OverlapPoint(hitS.point)) hitS= Physics2D.Raycast(m_preCollPos, m_velocity, Mathf.Infinity, 1 << explodeSurface.gameObject.layer);
            }
            if (hitS.collider != explodeSurface || hitS.collider == null) {
                explodePos = m_preCollPos;
            }
            else {
                explodePos = hitS.point + hitS.normal * 0.02f;
                m_explodeDir = hitS.normal;
                // if (hitS.collider.OverlapPoint(explodePos)){
                //     if (m_prePreCollPos!= Vector3.zero){
                //         hitS = Physics2D.CircleCast(m_prePreCollPos,gameObject.GetComponent<CircleCollider2D>().radius * 0.5f, 
                //                             m_velocity, Mathf.Infinity, 1 << explodeSurface.gameObject.layer);
                //         explodePos = hitS.point + hitS.normal * 0.02f;
                //     }
                //     else explodePos = m_preCollPos;
                // }
                Debug.DrawLine(Vector2.one, hitS.point, Color.yellow, 5.0f);
            }
        }
        else explodePos = m_preCollPos;
     
        return explodePos;
    }
    public void Explode(Vector3 explodePos){

        AudioManager.m_instance.PlaySound(m_explosionSound);
        transform.position = m_preCollPos;
        Debug.DrawLine(transform.position, explodePos, Color.red, 5.0f);
        Debug.DrawLine(Vector2.zero, explodePos, Color.red, 5.0f);
        //Play effects
        if (m_explodeDir == Vector3.zero) m_explodeDir = -transform.right;
        GameObject temp = EffectManager.m_instance.PlayExplosionEffect(explodePos);
        temp.transform.localScale = Vector2.one * m_explosionRadius * 1.5f;
        temp.transform.up = m_explodeDir;
        //Overlap to find explosion collisions
        Physics2D.OverlapCircle(explodePos, m_explosionRadius, m_affectedLayers, m_overlapResults);
        int myId = GetComponent<TeamEntityScript>().m_teamId;
        Debug.Log(m_overlapResults.Count);
        foreach (Collider2D hitCollider in m_overlapResults){

            //Chain reaction may cause objects to die before being iterated through
            if (!hitCollider.isActiveAndEnabled || hitCollider.gameObject == gameObject) continue;
            //find obstructions
            //Note: obstructions are decided by a raycast from the center of the affected collider to the explode position
            //This doesn't make sense for very big objects, may switch to fancast if possible
            //(Tf2 does this btw https://youtu.be/vfj-oE94cgs?si=GIKgRGu4X3O3ScgW&t=25)
            //(Can also use corners/sides https://youtu.be/vfj-oE94cgs?si=YCf3JgZm6ZAkIJ5W&t=134)
            Vector3 diff = hitCollider.transform.position - explodePos;
            RaycastHit2D obstruction = Physics2D.Raycast(explodePos, 
                                                diff, 
                                                (diff).magnitude,m_blockingLayers);
            if (obstruction) continue; 
            Debug.DrawLine(transform.position, hitCollider.transform.position, Color.green, 3.0f);
            Debug.Log(gameObject.name + " hitting " + hitCollider.name + " " + hitCollider.transform.position + " " + hitCollider.gameObject.layer);
            int surfaceHitCounts = Physics2D.Raycast(explodePos, 
                                                diff, 
                                                m_affectedLayers,
                                                m_raycastResults,
                                                diff.magnitude);
            if (surfaceHitCounts == 0) {
                Debug.DrawLine(transform.position, hitCollider.transform.position, Color.yellow, 3.0f);
                //Debug.Log()    
            }
            Vector2 explodeRayLocalPost = m_raycastResults[surfaceHitCounts - 1].point;
            if (hitCollider.OverlapPoint(explodePos)) explodeRayLocalPost = explodePos;
            Vector2 dir = explodeRayLocalPost - (Vector2)explodePos;

            if (hitCollider.GetComponent<Rigidbody2D>() != null){
                //if (hitCollider.GetComponent<BulletScript>() != null) continue;
                //Explosion force cal
                //Debug.Log(dir.magnitude + " " + hitCollider.gameObject.name);
                float distanceInverse = (m_explosionRadius - dir.magnitude) / m_explosionRadius;
                if (explodeRayLocalPost == (Vector2)hitCollider.transform.position) distanceInverse = 1;
                float exlosionForce = Mathf.Clamp(distanceInverse,m_minBaseExlosionForce, m_maxBaseExlosionForce); 

                Vector3 delta = dir;
                if (delta == Vector3.zero) delta = diff;
                delta.y += Mathf.Abs(0.5f * delta.y);
                if (delta == Vector3.zero) delta.y = 0.5f;
                Debug.Log("delta is " + delta);
                hitCollider.GetComponent<Rigidbody2D>().AddForceAtPosition(delta.normalized * exlosionForce * m_explosionForceScale,explodeRayLocalPost,ForceMode2D.Impulse);
                //hitCollider.GetComponent<Rigidbody2D>().AddForce(delta.normalized * exlosionForce * m_explosionForceScale,ForceMode2D.Impulse);
                Debug.Log(dir.normalized * exlosionForce * m_explosionForceScale); 
                Debug.Log(exlosionForce * m_explosionForceScale); 
                //Debug.DrawLine(transform.position,explodeRayLocalPost, Color.red, 3.0f);
                //Debug.Log(hitCollider.transform.position + " " + surfaceHit.collider.gameObject.name + " " + transform.position);
            }

            //Damage cal
            DamageData damageData = new DamageData(myId,0, explodePos);
            if (hitCollider.TryGetComponent<IDamageable>(out IDamageable idamageable)){
                //if (hitCollider.OverlapPoint(explodePos)) diff = Vector2.zero;
                dir = explodeRayLocalPost - (Vector2)explodePos;
                Debug.Log("diff is " + dir);
                float damage = Mathf.Min((m_explosionRadius - dir.magnitude) / m_explosionRadius, m_maxBaseExplosionDamage);
                int intDamage = Mathf.CeilToInt(damage * m_damageSlots);
                damage = intDamage * m_explosionDamageScale;
                //if (hitCollider.gameObject.layer == gameObject.layer) damage = 0;
                //if (damage < 8) Debug.Log(explodePos + " " + transform.position + " " + hitCollider.transform.position);
                damageData.m_damageAmount = -damage;
                idamageable.DamageBehaviour(damageData);
                //Debug.Log("Damage is " + damage);
            }
        }
        PostExplosionEvent();
        
    }
    protected virtual void PostExplosionEvent(){
        m_isFlaggedToDestroy = true;
        Destroy(gameObject);
    }
    public void Explode(Collider2D explodeSurface = null){
        Explode(FindExplodePos(explodeSurface));
    }

    protected virtual IEnumerator LifeTimeTracking(){
        if (m_lifeTime >= 0){
            yield return new WaitForSeconds(m_lifeTime);
            Explode();
        }
    }

    public void SetExcludeLayers(LayerMask TexcludeLayers){
        GetComponent<Collider2D>().excludeLayers = TexcludeLayers; 
    }
}
