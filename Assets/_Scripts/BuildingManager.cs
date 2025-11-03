using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class BuildingManager
    {
        private Building _prefab;
    
        // 만들어진 빌딩들을 모아서 관리할 Transform 배열
        private Transform[] _buildingLocators;
        
        // 새로 만들 빌딩들을 모두 저장해서 관리할 리스트를 생성
        private List<Building> _buildings = new List<Building>();
        
        // 생성자에서 필요한 디펜던시(의존성)를 주입하는 방식을 사용해 보려는 것
        public BuildingManager(Building prefab, Transform[] buildingLocators)
        {
            this._prefab = prefab;
            this._buildingLocators = buildingLocators;
        
            Debug.Assert(this._prefab != null, "null building prefab!");
            Debug.Assert(this._buildingLocators != null, "null buildingLocators!");

            // CreateBuildings();
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
                _buildings.Add(building);
            }
        }

        public void OnGameStarted()
        {
            CreateBuildings();
        }
    }
}