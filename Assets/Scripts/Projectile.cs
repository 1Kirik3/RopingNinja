using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected AudioClip m_collisionSound;
    [SerializeField] protected float m_projectileSpeed;
    [SerializeField] protected Transform m_mainPlatform;

    private Vector2 _movementVector;

    protected void ApplyMovement() 
        => transform.position = (Vector2)transform.position + (_movementVector * Time.deltaTime);

    protected void DetermineDirection()
    {
        if (Mathf.Abs(transform.position.x - m_mainPlatform.position.x) > Mathf.Abs(transform.position.y - m_mainPlatform.transform.position.y))
        {
            _movementVector = new Vector2(m_projectileSpeed, 0);
            _movementVector *= Mathf.Sign(m_mainPlatform.position.x - transform.position.x);
            if (m_mainPlatform.position.x - transform.position.x < 0)
                gameObject.transform.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            _movementVector = new Vector2(0, m_projectileSpeed);
            _movementVector *= Mathf.Sign(m_mainPlatform.position.y - transform.position.y);
            gameObject.transform.Rotate(0, 0, 90 * Mathf.Sign(m_mainPlatform.position.y - transform.position.y));
        }
    }

}
