using System;
using _01_Scripts.BulletLauncherSystem;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    private IGameController _gameController;

    public void SetGameController(IGameController gameController) // 오늘 가장 중요한 것
    {
        _gameController = gameController;
        if (_gameController == null)
            Debug.LogError("gameController is null");
    }

    private void Update()
    {
        if (_gameController != null)
        {
            if (_gameController.FireButtonPressed())
            {
                Debug.Log("발사");
            }
        }
        else
        {
            Debug.LogError("gameController is null2");
        }
    }
}