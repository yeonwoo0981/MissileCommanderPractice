using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Building : MonoBehaviour
    {
        private BoxCollider2D _boxCol;
    
        private void Awake()
        {
            _boxCol = GetComponent<BoxCollider2D>();
            _boxCol.isTrigger = true;
        }
    }
}