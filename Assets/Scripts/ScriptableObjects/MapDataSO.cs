using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData")]

public class MapDataSO :  ScriptableObject,IItemSelect
{
    public Sprite m_mapIconSprite;
    public CameraDataSO m_cameraData;
    public CamDataV2 m_cameraDataV2;
    public String m_mapScene;
    public String m_mapName;
    Sprite IItemSelect.DisplayIcon { 
        get => m_mapIconSprite; 
        set => m_mapIconSprite = value; 
    }
    public string DisplayName { 
        get => m_mapName;
        set => m_mapName = value; 
    }
}
