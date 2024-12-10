using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAIBehaviorScript : MonoBehaviour
{
    public float m_worthDisplay;
    public List<RaycastHit2D> m_scanList;
    public Collider2D[] m_scanWallList;
    public CooldownObjectScript m_cooldownObject;
 
    public AIWeaponData m_aiWeaponData;
  
    
    public void Awake(){
        m_scanList = new List<RaycastHit2D>(); 
        m_cooldownObject = GetComponent<CooldownObjectScript>();
    }

    public float CurrentWorth(float distance){
        float fin = Mathf.Clamp(Mathf.Abs(1/(distance-m_aiWeaponData.m_effectiveRange)), m_aiWeaponData.m_minDistanceClamp, m_aiWeaponData.m_maxDistanceClamp);
        if (distance > m_aiWeaponData.m_effectiveRange) fin *= m_aiWeaponData.m_effectiveRangeExceedMultiplier;
        if (m_cooldownObject.IsOnCooldown()) fin *= Mathf.Min(10,1 / m_cooldownObject.GetCurrentCooldown());
        else fin *= 10;
        return m_worthDisplay = m_aiWeaponData.m_worthScale  *
        fin;
    }
    public virtual Vector2 WeaponAIBehavior(Transform target, AI_STATE state){
        
        if (state == AI_STATE.ATTACK && (target.position - transform.position).magnitude <= m_aiWeaponData.m_scanRadius) {
            return (target.position - transform.position).normalized * 100;
        }
        Vector2 dir = Vector2.positiveInfinity;
        float tt = (state == AI_STATE.ATTACK)?Mathf.Infinity:0;
        bool picked = false;
        CustomCast.FanCast(transform.position, m_aiWeaponData.m_scanRadius, Vector2.up, 180, m_aiWeaponData.m_scanLayers,m_aiWeaponData.m_noneLayers,m_scanList,20,0.5f);
        //Debug.Log("SCAN " + m_scanList.Count);
        foreach(RaycastHit2D rh in m_scanList){
            float gg = (2 * transform.position - (Vector3)rh.point - target.position).magnitude ;
            if ((state == AI_STATE.ATTACK && tt > gg) || (state == AI_STATE.RETREAT && tt < gg)){
                tt = gg;
                dir = rh.point - (Vector2)transform.position;
                picked =true;
            }
        }
        if (!picked) dir = target.position - transform.position;
        return (dir.normalized + m_aiWeaponData.m_aimOffset) * 100;
    }
   
}
