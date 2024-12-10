using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/CameraData")]

public class CameraDataSO : ScriptableObject
{
    public float m_maxDistance;
    public float m_minZoom;
    public float m_maxZoom;
    public float m_margin;
    public float m_extraHeight;
    public Bounds m_cameraConstraints;
}
