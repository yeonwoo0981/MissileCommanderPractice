using System;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    protected bool IsActivated = false;

    protected Vector3 TargetPos;

    public Action<RecycleObject> Destroyed;

    // Æø¹ß¿ë
    public virtual void Activate(Vector3 pos)
    {
        IsActivated = true;
        transform.position = pos;
    }

    // ÃÑ¾Ë¿ë
    public virtual void Activate(Vector3 startPos, Vector3 targetPos)
    {
        transform.position = startPos;
        this.TargetPos = targetPos;

        Vector3 dir = (targetPos - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(transform.forward, dir);

        IsActivated = true;
    }
}
