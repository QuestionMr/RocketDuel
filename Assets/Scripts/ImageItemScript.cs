using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ImageData", menuName = "ScriptableObjects/ImageData")]

public class ImageItemSO : ScriptableObject, IItemSelect
{
    public Sprite m_imageSprite;
    public string m_imageName;

    public string DisplayName { 
        get => m_imageName;
        set => m_imageName = value; 
    }
    Sprite IItemSelect.DisplayIcon { 
        get => m_imageSprite; 
        set => m_imageSprite = value; 
    }
}
