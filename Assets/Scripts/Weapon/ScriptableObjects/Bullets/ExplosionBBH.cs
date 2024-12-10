using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Explosion", menuName = "ScriptableObjects/BulletBehaviours/ExplosionBBH", order = 1)]

public class ExplosionBBH : BulletBehavior
{
    public float m_explosionForce;
    public float m_explosionDamage;
    public float m_explosionDistanceDamageScaling;
    public override void Activate(GameObject target){
        
    }
}
