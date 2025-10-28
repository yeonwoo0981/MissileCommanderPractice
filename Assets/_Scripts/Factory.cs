using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    private List<RecycleObject> _pool = new List<RecycleObject>();
    private int _defaultPoolSize;
    private RecycleObject _prefab;

    // �ܺο��� ����
    public Factory(RecycleObject prefab, int defaultPoolSize = 5)
    {
        // �����ڸ� �̿��� ������� ������(������ ����)�� �⺻ ����
        this._prefab = prefab;
        this._defaultPoolSize = defaultPoolSize;

        // �ּ����� ������ġ
        Debug.Assert(this._prefab != null, "�������� ����");
    }

    // ���� �ѹ� ���� ���ִ� ��� �Լ�
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
        // _pool.Count�� 0�̸�, pool�� �ƹ��͵� ���ٸ�
        // CreatePool()�� ȣ���ؼ� �ʿ��� ���� ������Ʈ���� ���� �����Ѵ�
        if( _pool.Count == 0 )
        {
            CreatePool();
        }
        
        // ����� ������Ʈ�� pool���� ���ŵǹǷ�,
        // pool�ȿ��� �׻� "���� ������� ����" ������Ʈ�鸸 �����ְ� �ȴ�
        int lastIndex = _pool.Count - 1;
        RecycleObject obj = _pool[lastIndex];
        _pool.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    // ������Ʈ�� �ݳ��ϴ� �Լ�
    public void Restore(RecycleObject obj)
    {
        Debug.Assert(obj != null, "Null Object to be returned!");
        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }
}
