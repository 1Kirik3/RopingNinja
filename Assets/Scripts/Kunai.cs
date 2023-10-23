using UnityEngine;

public class Kunai : Projectile
{
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
        HealthPoints playerHealthPoints = collision?.GetComponent<HealthPoints>();
        if (playerHealthPoints)
            playerHealthPoints.TakeDamage();
    }

    private void OnBecameInvisible()
        => Destroy(gameObject);
}
