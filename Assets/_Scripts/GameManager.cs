using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �������� ���� ������ ����
    [SerializeField]
    private BulletLauncher launcherPrefab;

    [SerializeField] private Transform launcherLocator;
    
    // �ν��Ͻ��� ������ ���� ����
    private BulletLauncher _launcher;

    private void Start()
    {   
        // �Ѿ� �߻�� ����
        _launcher = Instantiate(launcherPrefab);
        
        _launcher.transform.position = launcherLocator.position;
        
        // MouseGameController�� �ٿ��ְ�
        MouseGameController mouseGameController = gameObject.AddComponent<MouseGameController>();

        // MouseGameController�� Action������ �Ѿ˹߻���� �Լ��� ����
        mouseGameController.FireButtonPressed += _launcher.OnFireButtonPressed;
    }
}
