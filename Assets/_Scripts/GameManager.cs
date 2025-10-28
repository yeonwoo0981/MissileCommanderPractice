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

    private void Start()
    {   
        // �Ѿ� �߻�� ����
        _launcher = Instantiate(launcherPrefab);

        // �Ѿ� �߻�� ��ġ ����
        _launcher.transform.position = launcherLocator.position;

        // MouseGameController�� �ٿ��ְ�
        MouseGameController mouseGameController = gameObject.AddComponent<MouseGameController>();

        // MouseGameController�� Action������ �Ѿ˹߻���� �Լ��� ����
        mouseGameController.FireButtonPressed += _launcher.OnFireButtonPressed;
    }
}
