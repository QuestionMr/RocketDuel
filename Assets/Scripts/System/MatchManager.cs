using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private Coroutine m_matchCooldownCoroutine;
    public CameraFollowScript m_cameraFollowScript;
    public CountdownUpdater m_countdownUpdater;
    
    public float m_countdown;
    private void ControlSetup(int id, bool cpu){
        GameObject player = PlayerManager.m_instance.m_players[id];
        IController mainController = player.GetComponent<AIControllerScript>();
        IController removedController = player.GetComponent<TouchControllerScript>();
        if (!cpu) (mainController, removedController) = (removedController, mainController);
        Destroy((UnityEngine.Object)removedController);
        player.GetComponent<PlayerScript>().m_controller = mainController;
    }
    void Start()
    {
        //GlobalManagerScript.m_instance.StartGame("TestMap", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        MapDataSO mapDataSO = MapItemManager.m_instance.m_selectedItems[0] as MapDataSO;
        GlobalManagerScript.m_instance.StartGame(mapDataSO.m_mapScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
       // m_cameraFollowScript.m_cameraData = mapDataSO.m_cameraData;
        m_cameraFollowScript.m_cameraData = mapDataSO.m_cameraDataV2;
        m_cameraFollowScript.CameraSetup();
        GetComponent<GlobalItemSpawnerScript>().AssignItems();
        GetComponent<GlobalItemSpawnerScript>().AssignWeaponCooldown();        
        for (int i = 0; i < PlayerManager.m_instance.m_players.Count;i++) ControlSetup(i, GameConfigSO.m_isCPU[i]); 
        InitMatch();
        //Time.timeScale = 0.1f;
    }

    private IEnumerator MatchCountdown(){
        GetComponent<PlayerManager>().SetPlayerControllable(false);
        m_countdownUpdater.gameObject.SetActive(true);
        m_countdownUpdater.StartCountdown(m_countdown);
        yield return new WaitForSeconds(m_countdown);
        GetComponent<PlayerManager>().SetPlayerControllable(true);

    }
    public void InitMatch(){
        GetComponent<PlayerManager>().InitPlayers();
        PauseManagerScript.m_instance.SetPlayState(PScreen.PLAY);
        if (m_matchCooldownCoroutine != null) StopCoroutine(m_matchCooldownCoroutine);
        m_matchCooldownCoroutine = StartCoroutine(MatchCountdown());
    }
}
