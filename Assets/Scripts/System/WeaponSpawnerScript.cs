using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnerScript : MonoBehaviour
{
    public WeaponManager m_weaponManager1;
    public List<WeaponItemDataSO> m_weaponItemData;
    // Start is called before the first frame update
    void Start()
    {
        foreach(WeaponItemDataSO weaponItemData in m_weaponItemData){
            GameObject wp = Instantiate(weaponItemData.m_weaponData.m_weaponPrefab, m_weaponManager1.transform);
            if (wp.GetComponent<WeaponScript>() == null){
                Debug.Log("INVALID WEAPON");
                continue;
            }
            WeaponScript wps = wp.GetComponent<WeaponScript>();
            wps.SetWeaponData(weaponItemData.m_weaponData);
            m_weaponManager1.AddNewWeapon(wps);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
