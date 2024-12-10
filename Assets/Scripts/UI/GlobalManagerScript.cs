using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManagerScript : MonoBehaviour
{
    public static GlobalManagerScript m_instance;
    private void Awake(){
        if (m_instance != null){
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        DontDestroyOnLoad(gameObject);
       
    }
    void Start()
    {
       
    }

    void Update()
    {
        
    }
    public void StartGame(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void StartGame(string sceneName, LoadSceneMode sceneMode){
        SceneManager.LoadScene(sceneName, sceneMode);
    }
}
