using System;
using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BulletLauncher bulletLauncher;

        private BulletLauncher _bulletLauncher;

        private void Start()
        {
            // 총알 발사대 생성: 마우스
            _bulletLauncher = Instantiate(bulletLauncher);

            MouseGameController mouseController = gameObject.AddComponent<MouseGameController>();
            mouseController.FireButtonPressed += _bulletLauncher.OnFireButtonPressed;
            
            // 키보드
            KeyGameController keyController = gameObject.AddComponent<KeyGameController>();
            keyController.FireButtonPressed += _bulletLauncher.OnFireButtonPressed;
        }

        private void OnDestroy()
        {
            
        }
    }
}