using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 프리팹을 담을 변수들 선언
    [SerializeField]
    private BulletLauncher launcherPrefab;

    // 인스턴스를 참조할 변수 선언
    private BulletLauncher _launcher;

    // 총알 발사대 수동 지정용 변수 선언
    [SerializeField] private Transform launcherLocator;

    private void Start()
    {   
        // 총알 발사대 생성
        _launcher = Instantiate(launcherPrefab);

        // 총알 발사대 위치 지정
        _launcher.transform.position = launcherLocator.position;

        // MouseGameController를 붙여주고
        MouseGameController mouseGameController = gameObject.AddComponent<MouseGameController>();

        // MouseGameController의 Action변수에 총알발사대의 함수를 연결
        mouseGameController.FireButtonPressed += _launcher.OnFireButtonPressed;
    }
}
