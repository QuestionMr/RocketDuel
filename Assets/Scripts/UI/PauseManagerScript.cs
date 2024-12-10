using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[Serializable]
public enum PScreen{
    PLAY,
    PAUSE,
    GAME_OVER
}
[Serializable]
public class PScreenData{
    public GameObject m_screenMenu;
    public bool m_screenPauseType;
}
[Serializable]
public class PScreenDataPair{
    public PScreen m_screenType;
    public PScreenData m_screenData;
}

public class PauseManagerScript : MonoBehaviour
{
    public static PauseManagerScript m_instance;
    private Dictionary<PScreen, PScreenData> m_screenDataDict;
    public List<PScreenDataPair> m_screenDataList;
    public TextMeshProUGUI m_gameOverText;
    public PScreen m_currentScreen;
    // public GameObject m_pauseMenu;
    // public GameObject m_playMenu;
    // Start is called before the first frame update
    private void Awake(){
          if (m_instance != null){
            m_instance = this;
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        if (m_screenDataDict == null) m_screenDataDict = new Dictionary<PScreen, PScreenData>();  
        m_screenDataDict.Clear();
        foreach(PScreenDataPair sdp in m_screenDataList){
            m_screenDataDict.Add(sdp.m_screenType, sdp.m_screenData);
        }
        //SetPlayState(false);     
    }

    public void SetPauseState(bool isPaused){
        Time.timeScale = isPaused? 0: 1;
        PlayerManager.m_instance.SetPlayerInteractable(!isPaused);
    }
    public void SetPlayState(PScreen state){
        m_screenDataDict[m_currentScreen].m_screenMenu.SetActive(false);
        
        m_currentScreen = state;

        PScreenData temp = m_screenDataDict[m_currentScreen];
        if (temp.m_screenMenu != null) temp.m_screenMenu.SetActive(true);
        SetPauseState(temp.m_screenPauseType);
    }
    public void SetPlayState(int state){
        PScreen stateScreen = (PScreen)state;
        SetPlayState(stateScreen);
    }
    public void SetGameOverText(int winId){
        m_gameOverText.text = "Player " + winId + " wins";
    }
}
