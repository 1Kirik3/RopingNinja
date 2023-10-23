using System;
using UnityEngine;

public class Coin : Projectile
{
    public static Action onCoinCollet;

    private void Start()
    {
        DetermineDirection();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory playerInventory = collision?.GetComponent<PlayerInventory>();
        if (playerInventory)
        {
            onCoinCollet?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
        => Destroy(gameObject);

}
