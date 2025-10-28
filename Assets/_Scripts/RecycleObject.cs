using System;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    protected bool IsActivated = false;

    protected Vector3 TargetPos;
    
    public Action<RecycleObject> Destroyed;

    // 폭발용
    public virtual void Activate(Vector3 pos)
    {
        IsActivated = true;
        transform.position = pos;
    }
    
    // 총알용
    public virtual void Activate(Vector3 startPos, Vector3 targetPos)
    {
        transform.position = startPos;
        TargetPos = targetPos;
        
        Vector3 dir = (targetPos - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(transform.forward, dir);

        IsActivated = true;
    }
}
