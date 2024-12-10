using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    DEAD,
    ALIVE
}
public class PlayerStateManager : MonoBehaviour
{
    public PlayerState m_currentState;
    public PlayerScript m_playerScript;
    // Start is called before the first frame update
    void Awake(){
    }
    public void SetState(PlayerState playerState){
        if (m_playerScript == null) m_playerScript = GetComponent<PlayerScript>();
        m_currentState = playerState;
        if (playerState == PlayerState.DEAD){
            SetPlayerAliveState(false);
            m_playerScript.m_controller.SetControllerState(false);
        }
        if (playerState == PlayerState.ALIVE){
            transform.up = Vector2.up;
            m_playerScript.ResetPlayer();
            m_playerScript.m_controller.SetControllerState(true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().totalForce = Vector2.zero;
            GetComponent<HealthComponent>().ResetHealth();
            SetPlayerAliveState(true);

        }
    }
    public void SetPlayerAliveState(bool isAlive){
        //GetComponent<Rigidbody2D>().constraints = isAlive?RigidbodyConstraints2D.FreezeRotation:RigidbodyConstraints2D.None;
        m_playerScript.enabled = isAlive;
    }
    
}
