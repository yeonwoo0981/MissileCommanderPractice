using System;
using Unity.Mathematics;
using UnityEngine;

namespace _01_Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;

        private void Start()
        {
            
        }

        private void Update()
        {
            // 최적화의 신이 되어보자.
            transform.position += transform.up * (_moveSpeed * Time.deltaTime);
        }

        public void Activate(Vector3 startPos, Vector3 targetPos)
        {
            startPos = transform.position;

            Vector3 dir = (targetPos - startPos).normalized;
            
            transform.rotation = Quaternion.LookRotation(transform.forward, dir);
        }
    }
}