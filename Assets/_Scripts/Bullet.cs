using System;
using UnityEngine;

public class Bullet : RecycleObject
{
    [SerializeField]
    float moveSpeed = 5f;


    private void Update()
    {
        if (!IsActivated)
            return;

        transform.position += transform.up * (moveSpeed * Time.deltaTime);

        if(IsArrivedToTarget())
        {
            IsActivated = false;

            // 이벤트 발생
            Destroyed?.Invoke(this);
        }
    }

    bool IsArrivedToTarget()
    {
        float distance = Vector3.Distance(transform.position, TargetPos);
        return distance < 0.1f;
    }
}
