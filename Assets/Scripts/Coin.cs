using System;
using UnityEngine;
using DG.Tweening;

public class Coin : Projectile
{

    public static Action onCoinCollet;
    [SerializeField] private float m_animationTime;
    private bool _isPicked = false;
    private Vector3 _rotationAngle = new Vector3(0, 360, 0);
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = GetComponent<Transform>().position;
        DetermineDirection();
        AnimateCoin();
    }

    private void FixedUpdate()
    {
        if (_isPicked == false)
        {
            ApplyMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory playerInventory = collision?.GetComponent<PlayerInventory>();
        if (playerInventory)
        {
            onCoinCollet?.Invoke();
            _isPicked = true;
            gameObject.transform.DOKill();
            gameObject.transform.DOMoveY(_startPosition.y + 1f, m_animationTime/2).SetEase(Ease.Linear);
            gameObject.transform.DORotate(_rotationAngle, m_animationTime/10).SetEase(Ease.Linear).SetLoops(3, LoopType.Incremental).OnComplete(DestroyCoin);
        }
    }

    private void AnimateCoin()
        => gameObject.transform.DORotate(_rotationAngle, m_animationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

    private void OnBecameInvisible()
    {
        DestroyCoin();
    }

    private void DestroyCoin()
    {
        gameObject.transform.DOKill();
        Destroy(gameObject);
    }
}
