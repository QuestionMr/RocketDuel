using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponScript> m_weapons;
    private int m_currentWeaponId = 0;
    public int m_invert = 1;
    private Vector2 m_currentPos;
    //public GameObject m_aimEffect;


    void Start(){
        m_currentWeaponId = 0;
        // foreach (WeaponScript wp in m_weapons){
        //     wp.GetComponent<TeamEntityScript>().m_teamId = GetComponent<TeamEntityScript>().m_teamId;
        // }
    }
    public WeaponScript GetCurrentWeapon(){
        return m_weapons[m_currentWeaponId];
    }
    public void ResetWeapons(){
        foreach (WeaponScript wp in m_weapons){
            wp.ResetCooldown();
        }
    }
 
    public void WeaponSwitch(){
        m_weapons[m_currentWeaponId].SetWeaponUsingState(false);
        m_currentWeaponId++;
        if (m_currentWeaponId >= m_weapons.Count) m_currentWeaponId = 0;
        m_weapons[m_currentWeaponId].SetWeaponUsingState(true);
        Debug.Log("SWAPPED");
    }
    public void OnStart(Vector2 pos){
        m_currentPos = pos;
    }

    public void OnHold(Vector2 pos){
        //Debug.Log("Weapon " + m_weapons[m_currentWeaponId].m_isAllowedToShoot);
        m_weapons[m_currentWeaponId].OnHold((pos - m_currentPos) * m_invert);

    }

    public void OnRelease(Vector2 pos){
        //m_weapons[m_currentWeaponId].DisplayAimEffect(false);
        m_weapons[m_currentWeaponId].OnRelease((pos - m_currentPos) * m_invert);
    }
    public void AddNewWeapon(WeaponScript wp){
        if (m_weapons == null) m_weapons = new List<WeaponScript>();
        m_weapons.Add(wp);
        wp.GetComponent<TeamEntityScript>().m_teamId = GetComponent<TeamEntityScript>().m_teamId;
        //wp.m_aimEffect = m_aimEffect.GetComponent<AimEffectScript>();

        // int currCnt = m_weapons.Count - 1;
        // CooldownManager.m_instance.AddNewCooldownObject(wp.m_cooldownObject, m_weaponIcons[currCnt]);
        // CooldownManager.m_instance.SetCooldownUIGraphic(wp.m_cooldownObject, wp.GetComponent<SpriteRenderer>().sprite);

    }
    public void OnDoubleTap(){
        WeaponSwitch();
    }
    public void WeaponVisibilityInitiation(){
        for (int i = 0; i < m_weapons.Count; i++){
            if (i == m_currentWeaponId) m_weapons[i].SetWeaponUsingState(true);
            else m_weapons[i].SetWeaponUsingState(false);
        }
    }
    public void SetWeaponVisibility(bool isVisible){
        for (int i = 0; i < m_weapons.Count; i++) m_weapons[i].SetWeaponUsingState(isVisible);
        
    }
}
