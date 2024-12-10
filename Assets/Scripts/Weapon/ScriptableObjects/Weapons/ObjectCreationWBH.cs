using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObjectCreation", menuName = "ScriptableObjects/WeaponBehaviors/ObjectCreationWBH", order = 1)]

public class ObjectCreationWBH : WeaponBehavior
{
    public GameObject m_creation;
    public override void Activate(GameObject player, Vector2 pos){
        Debug.Log("Created!");
    }
    public void Tests<T>(List<T> things){

    }
}
