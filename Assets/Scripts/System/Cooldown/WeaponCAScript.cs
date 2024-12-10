using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCAScript : MonoBehaviour, ICooldownAppearance
{
    public Sprite CooldownAppearance()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }
}
