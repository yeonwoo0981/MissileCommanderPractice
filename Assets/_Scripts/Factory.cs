using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    private List<RecycleObject> _pool = new List<RecycleObject>();
    private int _defaultPoolSize;
    private RecycleObject _prefab;

    // 외부에서 주입
    public Factory(RecycleObject prefab, int defaultPoolSize = 5)
    {
        // 생성자를 이용한 디펜던시 인젝션(의존성 주입)의 기본 패턴
        this._prefab = prefab;
        this._defaultPoolSize = defaultPoolSize;

        // 최소한의 안전장치
        Debug.Assert(this._prefab != null, "프리팹이 없다");
    }

    // 최초 한번 생성 해주는 기능 함수
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
        // _pool.Count가 0이면, pool에 아무것도 없다면
        // CreatePool()을 호출해서 필요한 게임 오브젝트들을 새로 생성한다
        if( _pool.Count == 0 )
        {
            CreatePool();
        }
        
        // 사용한 오브젝트는 pool에서 제거되므로,
        // pool안에는 항상 "아직 사용하지 않은" 오브젝트들만 남아있게 된다
        int lastIndex = _pool.Count - 1;
        RecycleObject obj = _pool[lastIndex];
        _pool.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    // 오브젝트를 반납하는 함수
    public void Restore(RecycleObject obj)
    {
        Debug.Assert(obj != null, "Null Object to be returned!");
        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }
}
