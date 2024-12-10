using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData/Base")]
public class WeaponDataSO : ScriptableObject
{
    public string m_type;
    public string m_itemName;
    public GameObject m_weaponPrefab;
    public float m_cooldown;

    public Sprite m_weaponSprite;
    public Sprite m_aimEffectSprite;
    public Sprite m_actionEffect;
    public Vector2 m_spriteScaling;

    public AudioClip m_weaponReleaseSoundEffect;
    public AIWeaponData m_aiWeaponData;

}
