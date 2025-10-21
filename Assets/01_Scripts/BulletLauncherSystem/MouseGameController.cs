using System;
using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class MouseGameController : MonoBehaviour, IGameController
    {
        public Action<Vector3> FireButtonPressed;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireButtonPressed?.Invoke(GetCurrentClickPoint(Input.mousePosition));
            }
        }
        
        private Vector3 GetCurrentClickPoint(Vector3 mousePos)
        {
            // 예외 처리를 꼭 하도록 하자
            if (Camera.main != null) 
                return Camera.main.ScreenToWorldPoint(mousePos);
            return Vector3.zero;
        }
    }
}