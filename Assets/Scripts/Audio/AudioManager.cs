using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip m_rocketAudio;
    public AudioClip m_readyAudio;
    public AudioClip m_goAudio;
    public AudioClip m_explosionAudio;
    public AudioClip m_contactAudio;
    public AudioClip m_meleeAudio;
    public AudioClip m_hitAudio;
    public AudioClip m_respawnAudio;
    public AudioClip m_buttonAudio;
    public AudioSource m_source;
    public static AudioManager m_instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (m_instance != null){
            Destroy(gameObject);
            return;
        }
        m_instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayRocketShootSound(){
        m_source.PlayOneShot(m_rocketAudio);
    }
    public void PlayExplosionSound(){
        m_source.PlayOneShot(m_explosionAudio);
    }
    public void PlayContactSound(float contactIntensity = 1f){
        m_source.PlayOneShot(m_contactAudio, contactIntensity);
    }
    public void PlayMeleeSound(){
        m_source.PlayOneShot(m_meleeAudio);
    }
    public void PlayHitSound(){
        m_source.PlayOneShot(m_hitAudio);
    }
    public void PlayRespawnSound(){
        m_source.PlayOneShot(m_respawnAudio);
    }
    public void PlaySound(AudioClip sound, float volume = 1){
        m_source.PlayOneShot(sound, volume);
    }
    public void PlayButtonSound(){
        m_source.PlayOneShot(m_buttonAudio);
    }
    public void PlayReadyAudio(){
        m_source.PlayOneShot(m_readyAudio);
    }
    public void PlayGoAudio(){
        m_source.PlayOneShot(m_goAudio);
    }
}
