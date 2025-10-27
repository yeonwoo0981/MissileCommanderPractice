using System;
using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class BulletLauncher : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        private Bullet _bullet;
        private Factory _bulletFactory;
        
        [SerializeField] float fireDelay = 0.5f;
        private float _elapsedFireTime;
        private bool _canShoot = true;
        
        [SerializeField] private Transform firePos;
        // 직렬화를 할 때는 언더바를 붙이지 말자
        
        private void Start()
        {
            _bulletFactory = new Factory(bulletPrefab);
        }
        
        public void OnFireButtonPressed(Vector3 pos)
        {
            if (!_canShoot)
                return;
            
            _bullet = _bulletFactory.Get() as Bullet;
            if (_bullet)
            {
                _bullet.Activate(firePos.position, pos);
                _bullet.Destroyed += OnBulletDestroyed;
            }
            _canShoot = false;
        }
        
        private void Update()
        {
            if (!_canShoot)
            {
                _elapsedFireTime += Time.deltaTime;
                if (_elapsedFireTime >= fireDelay)
                {
                    _canShoot = true;
                    _elapsedFireTime = 0f;
                }
            }
        }

        private void OnBulletDestroyed(Bullet usedBullet)
        {
            usedBullet.Destroyed -= OnBulletDestroyed;

            _bulletFactory.Restore(usedBullet);
        }

        private void OnDestroy()
        {
        }
    }
}