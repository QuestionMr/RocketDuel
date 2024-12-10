using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemBehaviorAdapter: MonoBehaviour
{
    public List<IItemSelect> m_items;
    public virtual void ItemInitialization(){

    }
}
// public class PlayerItemBehavior: ItemBehaviorAdapter
// {
//     public int m_playerIds;
//     public override void ItemInitialization(){
//         GameObject player = PlayerManager.m_instance.m_players[m_playerIds].gameObject;
//         IPlayerItemAssign tempI;
//         foreach(IItemSelect pItem in m_items){
//             tempI = pItem as IPlayerItemAssign;
//             tempI.AssignItemToPlayer(player);
//         }
//     } 
// }

