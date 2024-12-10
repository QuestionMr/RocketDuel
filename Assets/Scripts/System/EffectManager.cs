using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject m_hitEffectPrefab;
    public GameObject m_explosionEffectPrefab;
    public static EffectManager m_instance;
    void Awake(){
        if (m_instance != null && m_instance != this){
            Destroy(this);
            return;
        }
        m_instance = this;
    }

    public GameObject PlayCustomEffect(GameObject effect, Vector3 pos = default(Vector3), Quaternion rot = default(Quaternion)){
        return Instantiate(effect, pos, rot);
    }
    public GameObject PlayCustomEffect(GameObject effect, Transform parent, bool worldSpace = false){
        return Instantiate(effect,parent, worldSpace);
    }

    public GameObject PlayCustomTextEffect(GameObject effect, String text = "", Vector3 pos = default(Vector3), Quaternion rot = default(Quaternion)){
        GameObject temp = PlayCustomEffect(effect, pos, rot);
        temp.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().SetText(text);
        return temp;
    }

    public GameObject PlayHitEffect(String text = "", Vector3 pos = default(Vector3), Quaternion rot = default(Quaternion)){
        return PlayCustomTextEffect(m_hitEffectPrefab, text, pos, rot);
    }
    public GameObject PlayExplosionEffect(Vector3 pos = default(Vector3), Quaternion rot = default(Quaternion)){
        return PlayCustomEffect(m_explosionEffectPrefab, pos, rot);
    }
}
