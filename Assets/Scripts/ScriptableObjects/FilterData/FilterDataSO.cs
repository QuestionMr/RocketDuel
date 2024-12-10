using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FilterData", menuName = "ScriptableObjects/FilterData/Base")]

public class FilterDataSO : ScriptableObject
{
    public virtual bool FilterCheck(GameObject source, DamageData damageData){
        return true;
    }
}
