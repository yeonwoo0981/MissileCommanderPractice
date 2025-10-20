using System;
using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class MouseGameController : MonoBehaviour, IGameController
    {
        public Action FireButtonPressed;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireButtonPressed?.Invoke();
            }
        }
    }
}