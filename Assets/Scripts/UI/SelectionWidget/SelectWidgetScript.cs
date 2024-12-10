using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectWidgetScript : MonoBehaviour
{
    public List<IItemSelect> m_items;
    public ItemBehaviorAdapter m_itemBehaviorAdapter;
    public Image m_displayImage;
    public Image m_baseScalingImage;
    public TMPro.TextMeshProUGUI m_displayName;

    private int m_currentItemIndex;
    void Start(){
        //ConvertToIITemSelect(m_itemObjects);
    }
    void OnEnable(){
        IterateItems(0);
        //Debug.Log("WIDGET ENABLED");
    }
    public void DisplayCurrentItem(IItemSelect item){
        //Debug.Log(item.DisplayIcon.bounds + " " + item.DisplayIcon.name + " " + m_displayImage.sprite.bounds);
        float ratio = item.DisplayIcon.bounds.size.x / item.DisplayIcon.bounds.size.y;
        // Vector2 ogSize = m_displayImage.sprite.bounds.size;
        // m_displayImage.sprite = item.DisplayIcon;
        float mX =  Mathf.Min(m_baseScalingImage.rectTransform.sizeDelta.x, m_baseScalingImage.rectTransform.sizeDelta.y 
                                * ratio);
        m_displayImage.sprite = item.DisplayIcon;
        m_displayImage.rectTransform.sizeDelta = new Vector2(mX, mX / ratio);
        m_displayName.text = item.DisplayName;

    }

    public void IterateItems(int direction){
        m_currentItemIndex += direction;
        if (m_currentItemIndex < 0) m_currentItemIndex += m_items.Count;
        if (m_currentItemIndex >= m_items.Count) m_currentItemIndex -= m_items.Count;
        DisplayCurrentItem(m_items[m_currentItemIndex]);
    }

    public IItemSelect GetCurrentItem(){
        return m_items[m_currentItemIndex];
    }

    public void SetIItemSelectList(List<IItemSelect> items){
        m_items = items;
    }
    
    public void LockToAdapter(ItemBehaviorAdapter itemBehaviorAdapter){
        itemBehaviorAdapter.m_items.Add(GetCurrentItem());
    }
}
