using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void destroyObjectAndParent(){
        Destroy(gameObject.transform.parent.gameObject);
    }
}
