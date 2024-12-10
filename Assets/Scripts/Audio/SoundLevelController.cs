using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLevelController : MonoBehaviour
{
    public void OnEnable(){
        GetComponent<Slider>().value =  GameConfigSO.m_volume;

    }
    public void ChangeVolume(float vol){
        AudioManager.m_instance.m_source.volume = vol;
        GameConfigSO.m_volume = vol;
    }
}
