using System;
using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class TimeManager : MonoBehaviour
    {
        private bool _isGameStarted = false;
        
        public Action GameStarted;

        public void StartGame(float timeToStart = 3f)
        {
            if (_isGameStarted)
                return;
            
            _isGameStarted = true;

            StartCoroutine(DelayedGameStart(timeToStart));
        }

        private IEnumerator DelayedGameStart(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            
            GameStarted?.Invoke();
        }
    }
}