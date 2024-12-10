using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemManager : ItemManager
{
    public static MapItemManager m_instance;

    void Awake(){
        if (m_instance != null){
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        if (m_selectedItems == null) m_selectedItems = new List<IItemSelect>();
        m_selectedItems.Clear();
        DontDestroyOnLoad(gameObject);
       
    }
}
