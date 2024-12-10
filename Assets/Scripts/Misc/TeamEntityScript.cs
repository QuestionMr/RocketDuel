using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamEntityScript : MonoBehaviour
{
    public int m_teamId;
    public static bool CheckId(GameObject a, GameObject b){
        return (a.GetComponent<TeamEntityScript>() == null ||
                b.GetComponent<TeamEntityScript>() == null ||
                a.GetComponent<TeamEntityScript>().m_teamId != b.GetComponent<TeamEntityScript>().m_teamId
        );
    }
}
