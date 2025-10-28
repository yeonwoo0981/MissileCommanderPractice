using TMPro.EditorUtilities;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    private Bullet _bullet;

    [SerializeField] Explosion explosionPrefab;

    // 팩토리에서 관리하는 녀석들
    private Factory _bulletFactory;
    private Factory _explosionFactory;

    [SerializeField] Transform firePosition;


    // 쿨타임 적용에 필요한 변수들 선언
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

        RecycleObject _bullet = _bulletFactory.Get() as Bullet;
        if (_bullet)
        {
            _bullet.Activate(firePosition.position, pos);

            // 총알이 파괴될 때 이벤트를 바인딩(연결)
            _bullet.Destroyed += OnBulletDestroyed;
        }

        _canShoot = false;
    }

    private void OnBulletDestroyed(RecycleObject usedBullet)
    {
        // 총알이 회수되기전에 마지막 위치를 저장하고,
        Vector3 lastBulletPos = usedBullet.transform.position;

        // 파괴될 총알을 언바인딩(연결 해제)
        usedBullet.Destroyed -= OnBulletDestroyed;

        // 파괴된 총알은 다시 팩토리로 복귀 시킴
        _bulletFactory.Restore(usedBullet);

        // _explosionFactory.Get()을 통해 폭발효과 하나를 가져온 뒤, 마지막 위치에 위치시킨다
        RecycleObject explosion = _explosionFactory.Get();
        if (explosion)
        {
            explosion.Activate(lastBulletPos);
            explosion.Destroyed += OnExplosionDestroyed;
        }
    }

    private void OnExplosionDestroyed(RecycleObject usedExplosion)
    {
        usedExplosion.Destroyed -= OnExplosionDestroyed;
        _explosionFactory.Restore(usedExplosion);
    }
}
