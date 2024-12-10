using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessengerScript : MonoBehaviour
{
    public void LoadScene(string sceneName){
        GlobalManagerScript.m_instance.StartGame(sceneName);
    }
    public void LoadSceneAddictive(string sceneName){
        GlobalManagerScript.m_instance.StartGame(sceneName, LoadSceneMode.Additive);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
