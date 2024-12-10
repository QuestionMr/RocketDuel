using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownUpdater : MonoBehaviour
{
    public TextMeshProUGUI m_readyStateText;
    public TextMeshProUGUI m_timerText;
    public float m_timer;
    public float m_goTimerCheckpoint;
    public bool m_goPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
       
        if (m_timer <= 0) {
            gameObject.SetActive(false);
            return;
        }
        if (m_timer < m_goTimerCheckpoint && !m_goPlayed) {
            m_goPlayed = true;
            m_readyStateText.text = "GO";
            AudioManager.m_instance.PlayGoAudio();

        }
        m_timerText.text = m_timer.ToString("0.00");
    }
    public void StartCountdown(float s){
        m_timer = s;
        m_readyStateText.text = "READY";
        AudioManager.m_instance.PlayReadyAudio();
    }

}
