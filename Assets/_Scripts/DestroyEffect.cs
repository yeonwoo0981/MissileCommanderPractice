using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class DestroyEffect : RecycleObject
    {
        [SerializeField] private float effectTime = 0.5f;
        private float _elapsedTime = 0f;

        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            
            for (int i = 0; i < 100; i++)
            {
                float duration = Random.Range(0.5f, 2f);
                Vector3 randomScale = new Vector3(
                    Random.Range(0.5f, 3f),
                    Random.Range(0.5f, 3f),
                    1f
                );
                Vector3 randomMove = new Vector3(
                    Random.Range(-5f, 5f),
                    Random.Range(-5f, 5f),
                    0f
                );
                
                Ease randomEase = (Ease)Random.Range(0, (int)Ease.Flash + 1);
                
                sequence.Append(transform.DOScale(randomScale, duration).SetEase(randomEase));
                sequence.Append(transform.DOMove(randomMove, duration).SetEase(randomEase));
                sequence.Append(transform.DORotate(new Vector3(0f, 0f, Random.Range(0f, 360f)), duration, RotateMode.FastBeyond360)
                    .SetEase(randomEase));
            }

            sequence.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        private void Update()
        {
            if (!IsActivated)
                return;
            
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= effectTime)
            {
                _elapsedTime = 0f;
                IsActivated = false;
            
                Destroyed?.Invoke(this);
            }
        }
    }
}