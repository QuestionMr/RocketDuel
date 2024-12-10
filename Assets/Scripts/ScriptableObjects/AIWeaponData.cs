using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AIData", menuName = "ScriptableObjects/AIData")]

public class AIWeaponData : ScriptableObject
{ 
    public ContactFilter2D m_scanLayers;
    public ContactFilter2D m_noneLayers;
    public float m_scanRadius;
    public float m_effectiveRange;
    public float m_worthScale;
    public float m_minDistanceClamp;
    public float m_maxDistanceClamp;
    public float m_effectiveRangeExceedMultiplier;
    public Vector2 m_aimOffset;
}
