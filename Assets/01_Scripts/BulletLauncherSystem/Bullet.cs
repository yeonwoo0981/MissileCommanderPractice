using System;
using Unity.Mathematics;
using UnityEngine;

namespace _01_Scripts
{
    public class Bullet : RecycleObject
    {
        [SerializeField] private float _moveSpeed = 5f;

        private Vector3 _targetPos;

        private bool _isActivated = false;

        public Action<Bullet> Destroyed;
        private void Start()
        {
        }

        private void Update()
        {
            // 최적화의 신이 되어보자.
            if (!_isActivated)
                return;
            
            transform.position += transform.up * (_moveSpeed * Time.deltaTime);
            
            if (IsArrivedToTarget())
            {
                Destroyed?.Invoke(this);
                _isActivated = false;
            }
        }

        public void Activate(Vector3 startPos, Vector3 targetPos)
        {
            startPos = transform.position;
            
            _targetPos = targetPos;
            
            Vector3 dir = (targetPos - startPos).normalized;
            
            transform.rotation = Quaternion.LookRotation(transform.forward, dir);
            
            _isActivated = true;
        }
        
        private bool IsArrivedToTarget()
        {
            float distance = Vector3.Distance(transform.position, _targetPos);
            return distance < 0.1f;
        }
    }
}