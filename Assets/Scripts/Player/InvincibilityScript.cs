using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityScript : MonoBehaviour
{
    private BaseDamageableScript m_damageable;
    public float m_invulTime;
    // Start is called before the first frame update
    void Start()
    {
        m_damageable = GetComponent<BaseDamageableScript>();
    }

    public void StartInvul(){
        StopAllCoroutines();
        StartCoroutine(InvulCoroutine());
    }
    public void SetInvincibility(bool isInvul){
        m_damageable.SetMaxDamageCap(isInvul? 0:m_damageable.m_maxDamageReceive);
    }
    public IEnumerator InvulCoroutine(){
        SetInvincibility(true);
        GetComponent<SpriteManager>().SetColor(new Color(0.753f,0.753f,0.753f));
        yield return new WaitForSeconds(m_invulTime);
        SetInvincibility(false);
        GetComponent<SpriteManager>().ResetColor();

    }
}
