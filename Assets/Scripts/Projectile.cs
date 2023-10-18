using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] private Transform m_mainPlatform;

    private Vector2 _movementVector;

    private void Start()
    {
        DetermineDirection();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        transform.position = (Vector2)transform.position + (_movementVector * Time.deltaTime);
    }

    private void DetermineDirection()
    {
        if (Mathf.Abs(transform.position.x - m_mainPlatform.position.x) > Mathf.Abs(transform.position.y - m_mainPlatform.transform.position.y))
        {
            _movementVector = new Vector2(m_projectileSpeed, 0);
            _movementVector *= Mathf.Sign(m_mainPlatform.position.x - transform.position.x);
        }
        else
        {
            _movementVector = new Vector2(0, m_projectileSpeed);
            _movementVector *= Mathf.Sign(m_mainPlatform.position.y - transform.position.y);
        }
    }

}
