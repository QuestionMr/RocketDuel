using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerScript : MonoBehaviour
{
    public UnityEvent m_resetEvent;
    public IController m_controller;
    public void Start(){

    }

    public virtual void ResetPlayer(){
        //Debug.Log("resetting");
        GetComponent<WeaponManager>().ResetWeapons();
        m_resetEvent.Invoke();
    }
    public void SetPosition(Vector3 pos){
        transform.position = pos;
    }

}
