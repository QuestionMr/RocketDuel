using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINumberIterator : MonoBehaviour
{
    public TMP_InputField m_inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable(){
        m_inputField.text = GameConfigSO.m_lives.ToString();
    }
    public void SetInputValue(int dir){
        int g = 0;
        if (m_inputField.text.Length > 0) g = int.Parse(m_inputField.text);
        m_inputField.text = Mathf.Max(0, (g + dir)).ToString();
    }
}
