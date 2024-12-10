using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalItemSpawnerScript : MonoBehaviour
{
    void Start(){
       
        
    }
    public void AssignItems(){
        List<IItemSelect> selectedItems = PlayerItemManager.m_instance.m_selectedItems;
        List<int> m_selectedPlayerIds = PlayerItemManager.m_instance.m_selectedPlayerIds;
        for (int i = 0; i < selectedItems.Count; i++){
            IPlayerItemAssign asign = selectedItems[i] as IPlayerItemAssign;
            //Debug.Log(asign);
            asign.AssignItemToPlayer(PlayerManager.m_instance.m_players[m_selectedPlayerIds[i]]);
        }
        
    }
    public void AssignWeaponCooldown(){
        for (int i = 0; i < PlayerManager.m_instance.m_players.Count; i++){
            GameObject player = PlayerManager.m_instance.m_players[i];
            WeaponManager wm = player.GetComponent<WeaponManager>();
            wm.WeaponVisibilityInitiation();
            for (int j = 0; j < wm.m_weapons.Count; j++){
                Debug.Log(j + wm.m_weapons[j].name);
                CooldownConnectorScript.m_instance.SetObjectInPair(i,j, wm.m_weapons[j].m_cooldownObject);
            }
        }
        CooldownConnectorScript.m_instance.AddPoolToManager();
    }
}
