using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>(true);
        foreach (Button button in buttons){
            button.onClick.AddListener(AudioManager.m_instance.PlayButtonSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
