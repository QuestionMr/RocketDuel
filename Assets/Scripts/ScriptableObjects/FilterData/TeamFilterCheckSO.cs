using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TeamFilter", menuName = "ScriptableObjects/FilterData/TeamFilter")]

public class TeamFilterCheckSO : FilterDataSO
{
    public override bool FilterCheck(GameObject source, DamageData damageData){
        return source.GetComponent<TeamEntityScript>().m_teamId != damageData.m_damageTeamId;
    }
}
