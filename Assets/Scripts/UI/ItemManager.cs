using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<IItemSelect> m_selectedItems;
    public virtual void ImposeData(ItemSlot itemSlot){
        foreach (SelectWidgetScript widgetSlot in itemSlot.m_widgetSlots){
            m_selectedItems.Add(widgetSlot.GetCurrentItem());
        }

    }
}
