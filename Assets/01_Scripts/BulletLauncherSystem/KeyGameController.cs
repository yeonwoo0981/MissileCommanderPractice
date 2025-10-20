using System;
using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class KeyGameController : MonoBehaviour, IGameController
    {
        public Action FireButtonPressed;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireButtonPressed?.Invoke();
            }
        }
    }
}