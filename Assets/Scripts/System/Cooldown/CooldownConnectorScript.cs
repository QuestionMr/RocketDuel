using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class ImagePairList{
    public List<CooldownImagePair> m_cdPairList;
    public CooldownImagePair this[int key]{
        get{
            Debug.Log("get at" + key);
            return m_cdPairList[key];
        }
        set{
            m_cdPairList[key] = value;
        }
    }
}
public class CooldownConnectorScript : MonoBehaviour
{
    [SerializeField]
    public List<ImagePairList> m_cooldownImagePairPool;
    public static CooldownConnectorScript m_instance;
    void Awake()
    {
        if (m_instance != null){
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        if (m_cooldownImagePairPool == null) m_cooldownImagePairPool = new List<ImagePairList>();
    }
    public void SetObjectInPair(CooldownImagePair cooldownImagePair, CooldownObjectScript cooldownObject){
        Image childImage = cooldownImagePair.m_image.transform.GetChild(0).GetComponent<Image>();
        cooldownImagePair.m_cooldownObject = cooldownObject;
        if (cooldownObject.GetComponent<ICooldownAppearance>() != null) {
            //cooldownImagePair.m_image.sprite = 
            Sprite temp = cooldownObject.GetComponent<ICooldownAppearance>().CooldownAppearance();
            float ratio = temp.bounds.size.x / temp.bounds.size.y;
            
            float mX =  Mathf.Min(childImage.rectTransform.sizeDelta.x, childImage.rectTransform.sizeDelta.y 
                                    * ratio);
            childImage.sprite = temp;
            childImage.rectTransform.sizeDelta = new Vector2(mX, mX / ratio);
        }
    }
    public void SetObjectInPair(int poolId, int pairInPool, CooldownObjectScript cooldownObject){
        SetObjectInPair(m_cooldownImagePairPool[poolId][pairInPool], cooldownObject);

    }
    public void AddPoolToManager(){
        foreach (ImagePairList cooldownImagePairs in m_cooldownImagePairPool){
            foreach (CooldownImagePair cooldownImagePair in cooldownImagePairs.m_cdPairList){
                Debug.Log(cooldownImagePair.m_image.name);
                CooldownManager.m_instance.AddNewCooldownObject(cooldownImagePair);
            }
        }
    }
}
