using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class Missile : RecycleObject
    {
        private BoxCollider2D _box;
        private Rigidbody2D _body;

        private void Awake()
        {
            _box = GetComponent<BoxCollider2D>();
            _body = GetComponent<Rigidbody2D>();
        
            _body.bodyType = RigidbodyType2D.Kinematic;
            _box.isTrigger = true;
        }
    }
}