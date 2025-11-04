using System;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Building : MonoBehaviour
    {
        private BoxCollider2D _boxCol;

        public Action<Building> OnDestroyed;
        
        private void Awake()
        {
            _boxCol = GetComponent<BoxCollider2D>();
            _boxCol.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Missile>() != null)
            {
                BuildingDestroy();
            }
        }

        private void BuildingDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}