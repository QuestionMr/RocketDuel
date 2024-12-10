using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemBehavior: ItemBehaviorAdapter
{
    public int m_playerId;
    public override void ItemInitialization(){
        GameObject player = PlayerManager.m_instance.m_players[m_playerId].gameObject;
        IPlayerItemAssign tempI;
        foreach(IItemSelect pItem in m_items){
            tempI = pItem as IPlayerItemAssign;
            tempI.AssignItemToPlayer(player);
        }
    } 
}
