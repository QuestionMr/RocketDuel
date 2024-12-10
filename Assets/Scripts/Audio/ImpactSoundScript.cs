using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll){
        if (Mathf.Abs(coll.relativeVelocity.y) > 2f) {
            AudioManager.m_instance.PlayContactSound(Mathf.Abs(coll.relativeVelocity.y) * 0.2f);
            Debug.Log("PLAYINGSOUND");
        }

    }
}
