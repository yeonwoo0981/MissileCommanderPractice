using System;
using _Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �������� ���� ������ ����
    [SerializeField]
    private BulletLauncher launcherPrefab;

    // �ν��Ͻ��� ������ ���� ����
    private BulletLauncher _launcher;

    // �Ѿ� �߻�� ���� ������ ���� ����
    [SerializeField] private Transform launcherLocator;
    
    private MouseGameController _mouseGameController;

    [SerializeField] private Building buildingPrefab;
    [SerializeField] private Transform[] buildingLocators;
    private BuildingManager _buildingManager;
    
    [SerializeField] private Missile missilePrefab;
    private MissileManager _missileManager;

    private TimeManager _timeManager;
    
    [SerializeField] private int maxMissileCount = 20;
    [SerializeField] private float missileSpawnInterval = 0.5f;
    
    [SerializeField] private DestroyEffect effectPrefab;
    private void Start()
    {
        // �Ѿ� �߻�� ����
        _launcher = Instantiate(launcherPrefab);

        // �Ѿ� �߻�� ��ġ ����
        _launcher.transform.position = launcherLocator.position;

        // MouseGameController�� �ٿ��ְ�
        _mouseGameController = gameObject.AddComponent<MouseGameController>();

        _buildingManager = new BuildingManager(buildingPrefab, buildingLocators, new Factory(effectPrefab, 2));

        _timeManager = gameObject.AddComponent<TimeManager>();
        
        _missileManager = gameObject.AddComponent<MissileManager>();
        _missileManager.Initialize(new Factory(missilePrefab), _buildingManager, maxMissileCount, missileSpawnInterval);
        
        BindEvents();
        
        _timeManager.StartGame();
    }

    private void OnDestroy()
    {
        UnBindEvents();
    }
    
    private void BindEvents()
    {
        _mouseGameController.FireButtonPressed += _launcher.OnFireButtonPressed;
        _timeManager.GameStarted += _buildingManager.OnGameStarted;
        _timeManager.GameStarted += _launcher.OnGameStarted;
        _timeManager.GameStarted += _missileManager.OnGameStarted;
    }
    
    private void UnBindEvents()
    {
        _mouseGameController.FireButtonPressed -= _launcher.OnFireButtonPressed;
        _timeManager.GameStarted -= _buildingManager.OnGameStarted;
        _timeManager.GameStarted -= _launcher.OnGameStarted;
        _timeManager.GameStarted -= _missileManager.OnGameStarted;
    }
}
