using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private HealthBar m_playerHealthbar;
    [SerializeField] private int m_maxHealthpoints;
    public int CurrentHelth { get; private set; }

    private void Start()
    {
        CurrentHelth = m_maxHealthpoints;
    }

    public void TakeDamage()
    {
        CurrentHelth--;
        if (CurrentHelth <= 0)
            Destroy(gameObject);
        m_playerHealthbar.UpdateHealthbar();
    }

    public int GetMaxHealthpoints()
        => m_maxHealthpoints;
}
