using UnityEngine;
[CreateAssetMenu(fileName = "BaseWBH", menuName = "ScriptableObjects/WeaponBehaviors/BaseWBH", order = 1)]

public class WeaponBehavior : ScriptableObject
{
    public virtual void Activate(GameObject player, Vector2 dir){
        Debug.Log("Touched!");
    }
}
