using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExtraActionBridge : MonoBehaviour,IExtraAction
{
    public abstract void ExtraBehavior(Vector2 dir);

}
