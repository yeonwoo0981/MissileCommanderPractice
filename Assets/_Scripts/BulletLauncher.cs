using TMPro.EditorUtilities;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    private Bullet _bullet;

    [SerializeField] Explosion explosionPrefab;

    // ���丮���� �����ϴ� �༮��
    private Factory _bulletFactory;
    private Factory _explosionFactory;

    [SerializeField] Transform firePosition;


    // ��Ÿ�� ���뿡 �ʿ��� ������ ����
    [SerializeField] float fireDelay = 0.5f;
    float _elapsedFireTime;
    bool _canShoot = true;


    private void Start()
    {
        _bulletFactory = new Factory(bulletPrefab);
        _explosionFactory = new Factory(explosionPrefab);
    }

    private void Update()
    {
        if (!_canShoot)
        {
            _elapsedFireTime += Time.deltaTime;
            if (_elapsedFireTime >= fireDelay)
            {
                _canShoot = true;
                _elapsedFireTime = 0f;
            }
        }
    }

    public void OnFireButtonPressed(Vector3 pos)
    {
        if (!_canShoot)
            return;

        RecycleObject _bullet = _bulletFactory.Get();
        if (_bullet)
        {
            _bullet.Activate(firePosition.position, pos);

            // �Ѿ��� �ı��� �� �̺�Ʈ�� ���ε�(����)
            _bullet.Destroyed += OnBulletDestroyed;
        }

        _canShoot = false;
    }

    private void OnBulletDestroyed(RecycleObject usedBullet)
    {
        // �Ѿ��� ȸ���Ǳ����� ������ ��ġ�� �����ϰ�,
        Vector3 lastBulletPos = usedBullet.transform.position;

        // �ı��� �Ѿ��� ����ε�(���� ����)
        usedBullet.Destroyed -= OnBulletDestroyed;

        // �ı��� �Ѿ��� �ٽ� ���丮�� ���� ��Ŵ
        _bulletFactory.Restore(usedBullet);

        // _explosionFactory.Get()�� ���� ����ȿ�� �ϳ��� ������ ��, ������ ��ġ�� ��ġ��Ų��
        RecycleObject explosion = _explosionFactory.Get();
        if (explosion)
        {
            explosion.Destroyed += OnExplosionDestroyed;
            explosion.Activate(lastBulletPos);
        }
    }

    private void OnExplosionDestroyed(RecycleObject obj)
    {
        obj.Destroyed -= OnExplosionDestroyed;
        _explosionFactory.Restore(obj);
    }
}
