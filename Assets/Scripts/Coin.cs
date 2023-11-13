using System;
using UnityEngine;
using DG.Tweening;

public class Coin : Projectile
{

    public static Action onCoinCollet;
    [SerializeField] private float m_animationTime;
    private AudioSource _audioSource;
    private bool _isPicked = false;
    private Vector3 _rotationAngle = new Vector3(0, 360, 0);
    private Vector3 _startPosition;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
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
        if (collision.gameObject.layer == 7)
        {
            onCoinCollet?.Invoke();
            _audioSource.PlayOneShot(this.m_collisionSound);
            _isPicked = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.DOKill();
            gameObject.transform.DOMoveY(_startPosition.y + 1f, m_animationTime/2).SetEase(Ease.Linear);
            gameObject.transform.DORotate(_rotationAngle, m_animationTime/10).SetEase(Ease.Linear).SetLoops(3, LoopType.Incremental).OnComplete(DestroyCoin);
        }
    }

    private void AnimateCoin()
    {
        if (gameObject.transform != null)
            gameObject.transform.DORotate(_rotationAngle, m_animationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
        

    private void OnBecameInvisible()
    {
        DestroyCoin();
    }

    public void DestroyCoin()
    {
        gameObject.transform.DOKill();
        Destroy(gameObject);
    }
}
