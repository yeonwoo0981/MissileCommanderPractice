using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class Missile : RecycleObject
    {
        // 미사일 속도
        [SerializeField] private float moveSpeed = 3f;
        
        private BoxCollider2D _box;
        private Rigidbody2D _body;
        
        private void Awake()
        {
            _box = GetComponent<BoxCollider2D>();
            _body = GetComponent<Rigidbody2D>();
        
            _body.bodyType = RigidbodyType2D.Kinematic;
            _box.isTrigger = true;
        }

        private void Update()
        {
            if (!IsActivated)
                return;
            
            transform.position += transform.up * (moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Building>() != null)
            {
                Debug.Log("hit a building!");
                DestroySelf();
                return;
            }
            
            if (collision.GetComponent<Explosion>() != null)
            {
                Debug.Log("hit a Explosion!");
                DestroySelf();
            }
            
        }

        private void DestroySelf()
        {
            IsActivated = false;
            Destroyed?.Invoke(this);
        }
    }
}