using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunWP : WeaponScript
{
    public GameObject m_bulletPrefab;
    public Transform m_bulletSpawnPos;
    private Color m_owningPlayerColor;
    protected override void Awake()
    {
        base.Awake();
        m_owningPlayerColor = m_owningPlayer.GetComponent<SpriteRenderer>().color;
    }
    public override void ReleaseAction(Vector2 diff){
        base.ReleaseAction(diff);
        if (diff == Vector2.zero) return;
        GameObject gemp = Instantiate(m_bulletPrefab, m_bulletSpawnPos.position , Quaternion.identity);
        BulletScript temp = gemp.GetComponent<BulletScript>();
        temp.SetMovement(diff.normalized);
        temp.SetExcludeLayers(1 << gameObject.layer);
        temp.SetTrailColor(m_owningPlayerColor);
        temp.m_owningWeapon = gameObject;

        gemp.GetComponent<TeamEntityScript>().m_teamId = GetComponent<TeamEntityScript>().m_teamId;
    }
    public override void SetWeaponData(WeaponDataSO weaponDataSO){
        base.SetWeaponData(weaponDataSO);
        m_bulletPrefab = (weaponDataSO as GunBaseDataSO).m_bulletPrefab;
        //GetComponent<SpriteRenderer>().sprite = weaponDataSO.m_weaponSprite;
    }
}
