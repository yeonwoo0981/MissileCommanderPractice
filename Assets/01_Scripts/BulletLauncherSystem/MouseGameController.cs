using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class MouseGameController : IGameController
    {
        public bool FireButtonPressed()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}