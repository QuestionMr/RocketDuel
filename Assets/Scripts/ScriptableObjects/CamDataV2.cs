using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CameraDataV2", menuName = "ScriptableObjects/CameraDataV2")]

public class CamDataV2 : CameraDataSO
{
    public Bounds m_minConstrainst;
    public float m_extraTopRatio;
    public float m_extraBottomRatio;
    public float m_extraSideRatio;
}
