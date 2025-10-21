using _01_Scripts.BulletLauncherSystem;
using UnityEngine;

namespace _01_Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BulletLauncher bulletLauncher;

        private BulletLauncher _bulletLauncher;

        private void Start()
        {
            // 총알 발사대 생성
            _bulletLauncher = Instantiate(bulletLauncher);

            // 마우스
            MouseGameController mouseController = gameObject.AddComponent<MouseGameController>();
            mouseController.FireButtonPressed += _bulletLauncher.OnFireButtonPressed;
        }

        private void OnDestroy()
        {
            
        }
    }
}