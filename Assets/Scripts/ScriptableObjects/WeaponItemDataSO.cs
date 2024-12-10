using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponItemData", menuName = "ScriptableObjects/WeaponItemData/Base")]
public class WeaponItemDataSO : ScriptableObject,IItemSelect,IPlayerItemAssign
{
    public WeaponDataSO m_weaponData;

    public string DisplayName { 
        get => m_weaponData.m_itemName;
        set => m_weaponData.m_itemName = value; 
    }

    Sprite IItemSelect.DisplayIcon { 
        get => m_weaponData.m_weaponSprite; 
        set => m_weaponData.m_weaponSprite = value; 
    }

    public void AssignItemToPlayer(GameObject playerObject)
    {
        WeaponManager weaponManager = playerObject.GetComponent<WeaponManager>();
        if (weaponManager == null){
            Debug.Log("Can't find weapon manager");
            return;
        }
        GameObject wp = Instantiate(m_weaponData.m_weaponPrefab, weaponManager.transform);
        if (wp.GetComponent<WeaponScript>() == null){
            Debug.Log("INVALID WEAPON");
            return;
        }
        WeaponScript wps = wp.GetComponent<WeaponScript>();
        wps.SetWeaponData(m_weaponData);
        weaponManager.AddNewWeapon(wps);
    }
}
