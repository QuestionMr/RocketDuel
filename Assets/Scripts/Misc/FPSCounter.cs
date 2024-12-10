using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI m_fpsText;
    // [SerializeField] private float m_hudRefreshRate = 1f;

    // private float m_timer;

    // private void Update()
    // {
    //     if (Time.unscaledTime > m_timer)
    //     {
    //         int fps = (int)(1f / Time.unscaledDeltaTime);
    //         m_fpsText.text = "FPS: " + fps;
    //         m_timer = Time.unscaledTime + m_hudRefreshRate;
    //     }
    // }
     private float count;
    
    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private void OnGUI()
{
    Rect location = new Rect(25, 25, 850, 250);
    string text = $"FPS: {Mathf.Round(count)}";
    Texture black = Texture2D.linearGrayTexture;
    GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
    GUI.color = Color.black;
    GUI.skin.label.fontSize = 90;
    GUI.Label(location, text);
}
}