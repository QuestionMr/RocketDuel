using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> m_players;
    public List<Transform> m_spawnpoints;
    private Dictionary<int, int> m_teamStates;
    private int m_currentAliveTeamCount;
    public float m_respawnTimer;
    public static PlayerManager m_instance;

    private void Awake(){
        if (m_instance != null){
            Destroy(gameObject);
            return;
        }
        m_instance = this;
       
    }    
    public void InitPlayers(){
        foreach(GameObject player in m_players) {
            player.GetComponent<HealthComponent>().m_deathEvent.RemoveAllListeners();
            player.GetComponent<HealthComponent>().m_deathEvent.AddListener(CheckPlayerDeath);
            player.GetComponent<HealthComponent>().StockInit(GameConfigSO.m_lives);
        }
        ResetTeamStates();
        ResetAllPlayers();
    }
    void ResetTeamStates(){
        if (m_teamStates == null) m_teamStates = new Dictionary<int, int>();
        m_teamStates.Clear();
        foreach(GameObject player in m_players){
            int teamId = player.GetComponent<TeamEntityScript>().m_teamId;
            int temp;
            if (!m_teamStates.TryGetValue(teamId, out temp)) m_teamStates.Add(teamId, 1);
            else m_teamStates[teamId] = temp + 1;
            
        }
        m_currentAliveTeamCount = m_teamStates.Count;
    }
    public void ResetAllPlayers(){
        foreach(GameObject player in m_players){
            player.GetComponent<HealthComponent>().ResetStock();
            player.GetComponent<PlayerScript>().ResetPlayer();
            player.GetComponent<PlayerScript>().SetPosition(m_spawnpoints[m_players.IndexOf(player)].position);
        }
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.G)){
            ResetTeamStates();
            ResetAllPlayers();
        }
    }
    void LateUpdate(){
       
    }
    void CheckPlayerDeath(HealthComponent healthComponent){
        int temp;
        Debug.Log("DEATH");
        if (healthComponent.GetCurrentStocks() > 0){
            StartCoroutine(SpawnCoroutine(healthComponent.gameObject, m_respawnTimer));
            return;
        } 
        int teamId = healthComponent.GetComponent<TeamEntityScript>().m_teamId;
        if (m_teamStates.TryGetValue(teamId, out temp)) {
            if (temp == 0) return;
            m_teamStates[teamId] = temp - 1;
            if (temp == 1){
                m_currentAliveTeamCount--;
                if (m_currentAliveTeamCount <= 1){
                    PauseManagerScript.m_instance.SetPlayState(PScreen.GAME_OVER);
                    foreach (GameObject player in m_players){
                        if (player.GetComponent<HealthComponent>().m_currentStocks > 0){
                            PauseManagerScript.m_instance.SetGameOverText(player.GetComponent<TeamEntityScript>().m_teamId);
                        }
                    }
                    

                }
        }
            //Debug.Log(m_currentAliveTeamCount);
        }
    }

    private IEnumerator SpawnCoroutine(GameObject player, float timer){
        PlayerStateManager playerStateManager =player.GetComponent<PlayerStateManager>();
        playerStateManager.SetState(PlayerState.DEAD);
        yield return new WaitForSeconds(timer);
        AudioManager.m_instance.PlayRespawnSound();
        player.GetComponent<PlayerScript>().SetPosition(m_spawnpoints[m_players.IndexOf(player)].position);
        playerStateManager.SetState(PlayerState.ALIVE);
        player.GetComponent<InvincibilityScript>().StartInvul();
    }
    public void SetPlayerControllable(bool isControllable){
        foreach (GameObject player in m_players){
            player.GetComponent<PlayerScript>().enabled = isControllable;
            player.GetComponent<PlayerScript>().m_controller.SetControllerState(isControllable);
        }
    }
    public void SetPlayerInteractable(bool isInteractable){
        foreach (GameObject player in m_players){
            (player.GetComponent<IController>() as MonoBehaviour).enabled = isInteractable;
        }
    }
}
