using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    public void OnStart(Vector2 pos);

    public void OnHold(Vector2 pos);

    public void OnRelease(Vector2 pos);
    public void SetWeaponHoldingState(bool active);
    public void SetWeaponUsingState(bool active);
}
