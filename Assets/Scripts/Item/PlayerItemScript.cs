using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemScript : ScriptableObject,IItemSelect,IPlayerItemAssign
{    
    public Sprite m_itemSprite;
    public string m_itemName;

    public string DisplayName { 
        get => m_itemName;
        set => m_itemName = value; 
    }
    Sprite IItemSelect.DisplayIcon { 
        get => m_itemSprite; 
        set => m_itemSprite = value; 
    }

    public void AssignItemToPlayer(GameObject playerObject)
    {
        
    }
}
