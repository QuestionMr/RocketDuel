using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour , IWeapon
{
    public CooldownObjectScript m_cooldownObject;
    public Image m_weaponImage;
    protected GameObject m_owningPlayer;

    public AimEffectScript m_aimEffect;
    public List<ExtraActionBridge> m_onHolds; 
    public List<ExtraActionBridge> m_onReleases; 
    private bool m_isAllowedToShoot;
    private WeaponDataSO m_weaponDataSO;
    public bool m_test;
    public bool m_isHoldingWeapon;
    public bool m_isUsingWeapon;

    protected virtual void Awake(){
        m_isAllowedToShoot = true;
        m_cooldownObject.SetCurrentCooldown(0);
        m_owningPlayer = transform.parent.gameObject;
        //m_currentCooldown = -1;
        m_test = false;
        m_isHoldingWeapon = true;
        StartProcedure();
    }
    protected virtual void StartProcedure(){

    }
    void Update(){
        if (m_test) {
            ReleaseAction(transform.right);
            m_test = false;
        }
    }
    public void OnStart(Vector2 diff){
        
    }
    public virtual void OnHold(Vector2 diff)
    {
        //transform.right = Vector2.right;
        if (diff != Vector2.zero){
            m_aimEffect.SetEffectDirection(diff);
            transform.right = diff;
        }
        m_aimEffect.SetEffectVisibility(!m_cooldownObject.m_isOnCooldown && diff.magnitude > GameConfigSO.m_posThreshold);
        foreach(IExtraAction onHold in m_onHolds) onHold.ExtraBehavior(diff);
        
    }

    public void OnRelease(Vector2 diff)
    {
        m_aimEffect.SetEffectVisibility(false);
        if (m_cooldownObject.m_isOnCooldown || diff.magnitude <= GameConfigSO.m_posThreshold || !m_isHoldingWeapon) return;
        Debug.Log("Release " + m_isAllowedToShoot);
        ReleaseAction(diff);
        foreach(IExtraAction m_onRelease in m_onReleases) m_onRelease.ExtraBehavior(diff);
        m_cooldownObject.SetCooldownState(true);
    }

    public virtual void ReleaseAction(Vector2 diff){
        AudioManager.m_instance.PlaySound(m_weaponDataSO.m_weaponReleaseSoundEffect);

    }

    public void DisplayAimEffect(bool display){

        m_aimEffect.SetEffectVisibility(display);
    }

    public virtual void SetWeaponData(WeaponDataSO weaponDataSO){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<WeaponAIBehaviorScript>().m_aiWeaponData = weaponDataSO.m_aiWeaponData;
        m_weaponDataSO = weaponDataSO;
        m_cooldownObject.m_baseCooldown = weaponDataSO.m_cooldown;
        spriteRenderer.sprite = weaponDataSO.m_weaponSprite;
        float mX = weaponDataSO.m_spriteScaling.x / spriteRenderer.sprite.bounds.size.x;
        float mY = weaponDataSO.m_spriteScaling.y / spriteRenderer.sprite.bounds.size.y;
        mX = Mathf.Min(mX,mY);
        Vector3 ogScale = transform.localScale;
        Vector3 ogChildScale = m_aimEffect.transform.localScale;
        transform.localScale = new Vector3(mX, mX,1);
        m_aimEffect.transform.localScale= new Vector3(ogChildScale.x * ogScale.x/mX, ogChildScale.y * ogScale.y/mX, 1);
        gameObject.layer = transform.parent.gameObject.layer;
    }

    public void SetWeaponHoldingState(bool active){
        m_isHoldingWeapon = active;
        GetComponent<SpriteRenderer>().enabled = m_isHoldingWeapon & m_isUsingWeapon;
    }
    public void SetWeaponUsingState(bool active){
        m_isUsingWeapon = active;
        GetComponent<SpriteRenderer>().enabled = m_isHoldingWeapon & m_isUsingWeapon;
    }
    public void ResetCooldown(){
        m_cooldownObject.SetCooldownState(false);
    }
}
