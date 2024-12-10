using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControllerScript : MonoBehaviour,IController
{
    public WeaponManager m_weaponManager;
    private int m_currentTouch;
    public Rect m_touchArea;
    public float m_currentTapTime;
    public int m_tapCount;
    public void Start(){
        if (m_weaponManager == null) m_weaponManager = gameObject.GetComponent<WeaponManager>(); 
        m_weaponManager.m_invert = GameConfigSO.m_invert;

        int id = GetComponent<TeamEntityScript>().m_teamId;
        m_currentTouch = -1;
        m_touchArea.x = Screen.width / 2 * (id - 1);
        m_touchArea.width = Screen.width / 2;
        m_touchArea.height = Screen.height;

        m_touchArea.y = 0;
    }

    protected virtual void Update(){
        TouchInput();
    }

    void TouchInput(){
        bool hasTouch = false;
        if (Input.touchCount > 0){
            for (int i = 0; i < Input.touchCount; i++){
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began && m_currentTouch == -1 &&
                touch.position.x >= m_touchArea.position.x && touch.position.x <= m_touchArea.position.x + m_touchArea.width
                && touch.position.y >= m_touchArea.position.y && touch.position.y <= m_touchArea.position.y + m_touchArea.height
                && !EventSystem.current.IsPointerOverGameObject(touch.fingerId)){
                    if (m_tapCount == 1 && Time.time - m_currentTapTime <= GameConfigSO.m_doubleTapTime) {
                        m_weaponManager.OnDoubleTap();
                        m_tapCount = 0;
                        break;
                    }
                    m_currentTapTime = Time.time;
                    m_tapCount = 1;
                    
                    m_weaponManager.OnStart(touch.position);
                    m_currentTouch = touch.fingerId;
                    Debug.Log("fingerid " + touch.fingerId);
                    hasTouch = true;
                    break;
                }
                else if (touch.fingerId == m_currentTouch){
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary){
                        m_weaponManager.OnHold(touch.position);
                    }
                    if (touch.phase == TouchPhase.Ended){
                        m_currentTouch = -1;
                        //if (Time.time - m_currentTapTime <= m_doubleTapTime) break;
                        m_weaponManager.OnRelease(touch.position);
                    }
                    hasTouch = true;
                    break;
                }

            }
        }
        if (!hasTouch) m_currentTouch = -1;
    }

    public void SetControllerState(bool state)
    {
        this.enabled = state;
    }
}
