using System;
using UnityEngine;

namespace _01_Scripts.BulletLauncherSystem
{
    public class BulletLauncher : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        private Bullet _bullet;
        [SerializeField] private Transform firePos;
        // 직렬화를 할 때는 언더바를 붙이지 말자
        
        private void Start()
        {
            
        }
        
        public void OnFireButtonPressed(Vector3 pos)
        {
            _bullet = Instantiate(bulletPrefab);
            _bullet.Activate(firePos.position, pos);
            Debug.Log("불" + pos);
        }
        
        private void OnDestroy()
        {
            
        }
    }
}