using UnityEngine;

public class Kunai : Projectile
{
    private AudioSource _audioSource;
    private float _randomVolume;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _randomVolume = Random.Range(0.8f, 1f);
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
        {
            AudioSource.PlayClipAtPoint(this.m_collisionSound, transform.position, _randomVolume);
            _audioSource.PlayOneShot(this.m_collisionSound);
            playerHealthPoints.TakeDamage();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
        => Destroy(gameObject);
}
