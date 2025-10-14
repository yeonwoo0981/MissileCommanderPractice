using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class KeyGameController : IGameController
    {
        public bool FireButtonPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}