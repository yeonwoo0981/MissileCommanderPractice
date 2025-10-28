using System;
using UnityEngine;

public class MouseGameController : MonoBehaviour, IGameController
{
    public Action<Vector3> FireButtonPressed;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(FireButtonPressed != null)
            {
                // Vector3를 인자값으로 전달
                FireButtonPressed(GetCurrentClickPoint(Input.mousePosition));
            }
        }
    }

    Vector3 GetCurrentClickPoint(Vector3 mousePos)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(mousePos);
        point.z = 0f;
        return point;
    }
}
