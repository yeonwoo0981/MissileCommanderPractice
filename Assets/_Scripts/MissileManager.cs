using UnityEngine;

namespace _Scripts
{
    public class MissileManager : MonoBehaviour
    {
        // 1. 팩토리 (풀 매니저)
        // 2. BuildingManager (타겟팅 대상)
        
        private Factory _missileFactory;
        private BuildingManager _buildingManager;

        // 최초 단 한번만 생성될 수 있도록 하기 위함
        private bool _isInitialized = false;
    
        // 외부에서 주입하여 생성자처럼 사용하기 위함 
        public void Initialize(Factory missileFactory, BuildingManager buildingManager)
        {
            if (_isInitialized)
                return;
        
            this._missileFactory = missileFactory;
            this._buildingManager = buildingManager;
        
            Debug.Assert(this._missileFactory != null, "missile factory is null!");
            Debug.Assert(this._buildingManager != null, "building manager is null!");

            _isInitialized = true;
        }

        private void SpawnMissile()
        {
            Debug.Assert(this._missileFactory != null, "missile factory is null!");
            Debug.Assert(this._buildingManager != null, "building manager is null!");
        
            RecycleObject missile = _missileFactory.Get();
            missile.Activate(GetMissileSpawnPosition(), _buildingManager.GetRandomBuildingPosition());
            missile.Destroyed += OnMissileDestroyed;
        }

        private void OnMissileDestroyed(RecycleObject missile)
        {
            missile.Destroyed -= OnMissileDestroyed;
            _missileFactory.Restore(missile);
        }

        private Vector3 GetMissileSpawnPosition()
        {
            Vector3 spawnPosition = Vector3.zero;
            spawnPosition.x = Random.Range(0f, 1f);
            spawnPosition.y = 1f;
    
            if (Camera.main != null)
            {
                spawnPosition = Camera.main.ViewportToWorldPoint(spawnPosition);
            }
    
            spawnPosition.z = 0f;
    
            return spawnPosition;
        }
        
        public void OnGameStarted()
        {
            SpawnMissile();
        }
    }
}