using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScript : MonoBehaviour
{
    public static SingletonScript instance;
    void Awake(){
        if (instance != null && instance != this){
            Destroy(this);
            return;
        }
        instance = this;
    }
}
