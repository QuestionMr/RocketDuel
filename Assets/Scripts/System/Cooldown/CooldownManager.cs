using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class CooldownImagePair{
    public CooldownImagePair(CooldownObjectScript c, Image i){
        m_image = i;
        m_cooldownObject = c;
    }
    public Image m_image;
    public CooldownObjectScript m_cooldownObject;
}
public class CooldownManager : MonoBehaviour
{
    public static CooldownManager m_instance;
    [SerializeField]
    //public List<CooldownImagePair> m_cooldownImagePairs;
    private Dictionary<CooldownObjectScript, Image> m_cooldownDict;
    private List<CooldownObjectScript> m_activeCooldownObjects;
    void Awake()
    {
        if (m_instance != null){
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        m_cooldownDict = new Dictionary<CooldownObjectScript, Image>();
        if (m_activeCooldownObjects == null) m_activeCooldownObjects = new List<CooldownObjectScript>();
    }

    void LateUpdate()
    {
        for (int i = 0; i < m_activeCooldownObjects.Count; i++){
            if (!m_activeCooldownObjects[i].IsOnCooldown()){
                m_activeCooldownObjects[i].SetCooldownState(false);
                //SetCooldownImageAlpha(m_activeCooldownObjects[i], 1);
                m_activeCooldownObjects.RemoveAt(i);
                i--;
            }
            else {
                m_activeCooldownObjects[i].UpdateCooldown();
                m_cooldownDict[m_activeCooldownObjects[i]].fillAmount = m_activeCooldownObjects[i].CooldownRatio();
            }
        }
    }

    void SetCooldownImageState(bool state){

    }
    public void InitCooldownConnection(){
        // foreach (CooldownImagePair cooldownImagePair in m_cooldownImagePairs){
        //     m_cooldownDict.Add(cooldownImagePair.m_cooldownObject,cooldownImagePair.m_image);
        // }
        // foreach (KeyValuePair<CooldownObjectScript,Image> cooldownObjectPair in m_cooldownDict){
        //     cooldownObjectPair.Key.m_startCooldownEvent.AddListener(AddActiveCooldownObject);
        // }
    }
    void AddActiveCooldownObject(CooldownObjectScript cooldownObjectScript){
        //if (m_activeCooldownObjects == null) m_activeCooldownObjects = new List<CooldownObjectScript>();
        m_activeCooldownObjects.Add(cooldownObjectScript);
        //SetCooldownImageAlpha(cooldownObjectScript, 0.1f);
        
    }
    private void SetCooldownImageAlpha(CooldownObjectScript cooldownObjectScript, float alphaValue){
        Image image;
        if (m_cooldownDict.TryGetValue(cooldownObjectScript, out image)){
            var tempColor = image.color;
            tempColor.a = alphaValue;
            image.color = tempColor;
        }
    }
    public void AddNewCooldownObject(CooldownObjectScript cooldownObjectScript, Image image){
        //if (cooldownObjectScript == null) Debug.Log("LLLLL ");
        m_cooldownDict.Add(cooldownObjectScript,image);
        cooldownObjectScript.m_cooldownStartEvent.AddListener(AddActiveCooldownObject); 
    }


    public void AddNewCooldownObject(CooldownImagePair cooldownImagePair){
        AddNewCooldownObject(cooldownImagePair.m_cooldownObject, cooldownImagePair.m_image);
    }
    public void SetCooldownUIGraphic(CooldownObjectScript cooldownObjectScript, Sprite sprite){
        Image image;
        if (m_cooldownDict.TryGetValue(cooldownObjectScript, out image)){
            image.sprite = sprite;
        }
    }
}
