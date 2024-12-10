using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberCounterScript : MonoBehaviour
{
    public TextMeshProUGUI m_numberText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeNumber(int num){
        m_numberText.text = num.ToString();
    }
}
