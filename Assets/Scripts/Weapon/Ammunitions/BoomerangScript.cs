using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : BulletScript
{
    public bool m_returnState;
    public float m_minReturnRadius;
    public float m_returnTime;
    public Rigidbody2D m_rb;
    public float m_forceMultiplier;
    public float m_maxAttractDis;
    public int m_maxExplosions;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ReturnTracking());
        m_rb = GetComponent<Rigidbody2D>();
        m_owningWeapon.GetComponent<CooldownObjectScript>().m_isCooldownPaused = true;
        m_owningWeapon.GetComponent<WeaponScript>().SetWeaponHoldingState(false);
    }
    void Update(){
        if (m_returnState){
            Vector3 diff = transform.position - m_owningWeapon.transform.position;
            float dist = Mathf.Min(diff.magnitude, m_maxAttractDis);
            m_rb.AddForce(-diff.normalized / (dist * dist) * Time.deltaTime * m_forceMultiplier);
            //transform.position = Vector3.MoveTowards(transform.position, m_owningPlayer.transform.position, m_forceMultiplier * Time.deltaTime);
            Debug.DrawLine(transform.position,  m_owningWeapon.transform.position,Color.red);
            if (diff.magnitude <= m_minReturnRadius) {
                m_owningWeapon.GetComponent<CooldownObjectScript>().m_isCooldownPaused  = false;
                m_owningWeapon.GetComponent<WeaponScript>().SetWeaponHoldingState(true);
                m_owningWeapon.GetComponent<WeaponScript>().ResetCooldown();

                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll){
        if (m_maxExplosions <= 0) return;
        //if (!m_returnState) 
        Debug.Log("mn " + coll.contactCount);
        Vector3 temp = coll.GetContact(0).point;
        if (coll.collider.OverlapPoint(temp)){
            temp = FindExplodePos(coll.collider);
        }
        // for(int i = 0; i < coll.contactCount; i++){
        //     Debug.DrawLine(Vector2.zero, coll.GetContact(i).point, Color.green, 5.0f);
        // }
        Explode(temp);
        m_maxExplosions--;

    }
    protected override void PostExplosionEvent()
    {
        //m_returnState = true;
    }
    protected virtual IEnumerator ReturnTracking(){
        yield return new WaitForSeconds(m_returnTime);
        m_returnState = true;
    }




}
