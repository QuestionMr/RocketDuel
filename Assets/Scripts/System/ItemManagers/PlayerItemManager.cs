using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : ItemManager
{
    public static PlayerItemManager m_instance;
    public List<int> m_selectedPlayerIds;

    void Awake(){
        if (m_instance != null){
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        if (m_selectedPlayerIds == null) m_selectedPlayerIds = new List<int>();
        m_selectedPlayerIds.Clear();
        if (m_selectedItems == null) m_selectedItems = new List<IItemSelect>();
        m_selectedItems.Clear();
        DontDestroyOnLoad(gameObject);
       
    }

    public override void ImposeData(ItemSlot itemSlot){
        base.ImposeData(itemSlot);
        foreach (int temp in (itemSlot as PlayerItemSlot).m_playerId){
            m_selectedPlayerIds.Add(temp);
        }

    }
}
