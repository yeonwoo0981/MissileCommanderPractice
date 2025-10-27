using UnityEngine;
using System;
using System.Collections.Generic;

namespace _01_Scripts
{
    public class Factory
    {
        private List<RecycleObject> _pool = new List<RecycleObject>();
        private int _defaultPoolSize;
        private RecycleObject _prefab;

        public Factory(RecycleObject prefab, int defaultPoolSize = 5)
        {
            this._prefab = prefab;
            this._defaultPoolSize = defaultPoolSize;
            
            // if null 돌릴 필요 없이 이걸로 하면 편할 듯
            Debug.Assert(this._prefab != null, "Prefab is null!!!!!!");
        }
        
        private void CreatePool()
        {
            for (int i = 0; i < _defaultPoolSize; i++)
            {
                RecycleObject obj = GameObject.Instantiate(_prefab);
                obj.gameObject.SetActive(false);
                _pool.Add(obj);
            }
        }
        
        public RecycleObject Get()
        {
            if (_pool.Count == 0)
            {
                CreatePool();
            }

            int lastIndex = _pool.Count - 1;
            RecycleObject obj = _pool[lastIndex];
            _pool.RemoveAt(lastIndex);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public RecycleObject Restore(RecycleObject obj)
        {
            Debug.Assert(obj != null, "Null object is 머시기");
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
            return obj;
        }
    }
}