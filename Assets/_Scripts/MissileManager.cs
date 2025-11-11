using System.Collections;
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
    
        /* 
         * 1. 한번에 생성 가능한 미사일 갯수 제한
         * 2. 미사일 사이 생성 쿨타임
         * 3. 현재까지 생성된 미사일 갯수
         * Dataerr0r
         */
        
        private int _maxMissileCount = 20;
        private int _currentMissileCount;
        private float _missileSpawnInterval = 0.5f;

        // 이렇게 할 필요는 없지만 편의성을 위해서
        private Coroutine _autoSpawnMissileCoroutine;
        
        // 외부에서 주입하여 생성자처럼 사용하기 위함 
        public void Initialize(Factory missileFactory, BuildingManager buildingManager,
            int maxMissileCount, float missileSpawnInterval)
        {
            if (_isInitialized)
                return;
        
            this._missileFactory = missileFactory;
            this._buildingManager = buildingManager;
            this._maxMissileCount = maxMissileCount;
            this._missileSpawnInterval = missileSpawnInterval;
            
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
            
            _currentMissileCount++;
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
            //SpawnMissile();
            
            _currentMissileCount = 0;
            _autoSpawnMissileCoroutine = StartCoroutine(AutoSpawnMissile());
        }

        private IEnumerator AutoSpawnMissile()
        {
            while (_currentMissileCount < _maxMissileCount)
            {
                yield return new WaitForSeconds(_missileSpawnInterval);

                if (_buildingManager.HasBuilding == false)
                {
                    Debug.LogWarning("모든 빌딩 파괴");
                    yield break;
                }
                SpawnMissile();
            }
        }
    }
}