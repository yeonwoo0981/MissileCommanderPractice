using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent (typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Explosion : RecycleObject
{
    private CircleCollider2D _circle;
    private Rigidbody2D _body;

    [SerializeField] float timeToRemove = 1f;
    private float _elapsedTime = 0f;
    private bool _isActivated = false;
    
    void Awake()
    {
        _circle = GetComponent<CircleCollider2D>();
        _body = GetComponent<Rigidbody2D>();

        _circle.isTrigger = true;
        _body.bodyType = RigidbodyType2D.Kinematic;

                    _circle = GetComponent<CircleCollider2D>();
                    _body = GetComponent<Rigidbody2D>();
                    
                    _circle.isTrigger = true;
                    _body.bodyType = RigidbodyType2D.Kinematic;
                    
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

    // Update is called once per frame
    void Update()
    {
        if (_isActivated)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= timeToRemove)
            {
                _elapsedTime = 0f;
                DestroySelf();
            }
        }
    }
    
    private void DestroySelf()
    {
        _isActivated = false;
        Debug.Log(gameObject.name + "이 파괘됬다!!");
        Destroyed?.Invoke(this);
    }
}
