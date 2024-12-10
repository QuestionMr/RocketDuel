using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ItemSlot{
    public List<SelectWidgetScript> m_widgetSlots;
    public List<ScriptableObject> m_itemObjectPool;
    public List<IItemSelect> m_convertedItemPool;
}
[System.Serializable]
public class PlayerItemSlot: ItemSlot{
    public List<int> m_playerId;
}
public class ItemSelectionMessengerScript : MonoBehaviour
{
    public List<PlayerItemSlot> m_playerItemSlots;
    public List<ItemSlot> m_mapItemSlots;
    public List<ItemSlot> m_imageItemSlots;
    public List<Toggle> m_cpuToggles;
    public TMP_InputField m_inputField;
    
    public void AssignItems(){
        //ItemManager.m_instance.AssignItems(m_playerItemSlots);
    }
    public void SetSelectedItems(){
        PlayerItemManager.m_instance.m_selectedItems.Clear();
        MapItemManager.m_instance.m_selectedItems.Clear();
        foreach (PlayerItemSlot playerItemSlot in m_playerItemSlots){
            PlayerItemManager.m_instance.ImposeData(playerItemSlot);
        }
        foreach (ItemSlot mapItemSlot in m_mapItemSlots){
            MapItemManager.m_instance.ImposeData(mapItemSlot);
        }
        for (int i = 0; i < m_cpuToggles.Count; i++){
            GameConfigSO.m_isCPU[i] = m_cpuToggles[i].isOn;
        }
        if (m_inputField.text == "") GameConfigSO.m_lives = 3;
        else GameConfigSO.m_lives = int.Parse(m_inputField.text);
    }
    public void ConvertToIITemSelect(ItemSlot itemSlot){
            if (itemSlot.m_convertedItemPool == null) itemSlot.m_convertedItemPool = new List<IItemSelect>();
            foreach (ScriptableObject so in itemSlot.m_itemObjectPool){
                if (so is IItemSelect tempItemSelect && !itemSlot.m_convertedItemPool.Contains(tempItemSelect)) 
                    itemSlot.m_convertedItemPool.Add(tempItemSelect);
            }
            foreach(SelectWidgetScript ws in itemSlot.m_widgetSlots){
                ws.SetIItemSelectList(itemSlot.m_convertedItemPool);
            }
        
    }
    public void SetHazardState(bool state){
        GameConfigSO.m_hazard = state;
    }
    void Start(){
        foreach (PlayerItemSlot playerItemSlot in m_playerItemSlots)ConvertToIITemSelect(playerItemSlot);
        foreach (ItemSlot itemSlot in m_mapItemSlots)ConvertToIITemSelect(itemSlot);
        foreach (ItemSlot itemSlot in m_imageItemSlots)ConvertToIITemSelect(itemSlot);
    }
    void OnEnable(){
        for (int i = 0; i < m_cpuToggles.Count; i++) m_cpuToggles[i].isOn = GameConfigSO.m_isCPU[i];

    }
}
