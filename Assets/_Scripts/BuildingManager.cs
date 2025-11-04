using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts
{
    public class BuildingManager
    {
        private Building _prefab;
        
        private Factory _effectFactory;
    
        // 만들어진 빌딩들을 모아서 관리할 Transform 배열
        private Transform[] _buildingLocators;
        
        // 새로 만들 빌딩들을 모두 저장해서 관리할 리스트를 생성
        private List<Building> _buildings = new List<Building>();
        
        // 생성자에서 필요한 디펜던시(의존성)를 주입하는 방식을 사용해 보려는 것
        public BuildingManager(Building prefab, Transform[] buildingLocators, Factory effectFactory)
        {
            this._prefab = prefab;
            this._buildingLocators = buildingLocators;
            this._effectFactory = effectFactory;
            
            Debug.Assert(this._prefab != null, "null building prefab!");
            Debug.Assert(this._buildingLocators != null, "null buildingLocators!");
            Debug.Assert(this._effectFactory != null, "null effectFactory");
        }

        private void CreateBuildings()
        {
            if (_buildings.Count > 0)
            {
                Debug.LogWarning("Buildings have been already created!");
                return;
            }
    
            for (int i = 0; i < _buildingLocators.Length; i++)
            {
                Building building = Object.Instantiate(_prefab);
                building.transform.position = _buildingLocators[i].position;
                building.OnDestroyed += OnBuildingDestroyed;
                _buildings.Add(building);
            }
        }

        private void OnBuildingDestroyed(Building building)
        {
            Debug.Log("빌딩파괴");
            building.OnDestroyed -= OnBuildingDestroyed;

            Vector3 lastPos = building.transform.position;
            lastPos.y += building.GetComponent<BoxCollider2D>().size.y * 0.5f;
            
            int index = _buildings.IndexOf(building);
            _buildings.RemoveAt(index);
            Object.Destroy(building.gameObject); // DestroyImmediate 라이프 사이클을 보고 사용해라

            RecycleObject effect = _effectFactory.Get();
            effect.Activate(lastPos);
            effect.Destroyed += this.OnEffectDestroyed;
        }

        private void OnEffectDestroyed(RecycleObject effect)
        {
            effect.Destroyed -= OnEffectDestroyed;
            _effectFactory.Restore(effect);
        }

        public Vector3 GetRandomBuildingPosition()
        {
            Debug.Assert(_buildings.Count > 0, "no element in buildings!");
    
            Building building = _buildings[Random.Range(0, _buildings.Count)];
    
            return building.transform.position;
        }

        public void OnGameStarted()
        {
            CreateBuildings();
        }
    }
}