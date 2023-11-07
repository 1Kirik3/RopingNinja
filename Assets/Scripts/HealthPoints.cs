using UnityEngine;
using DG.Tweening;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private HealthBar m_playerHealthbar;
    [SerializeField] private int m_maxHealthpoints;
    [SerializeField] private GameObject m_deathCanvas;
    public int CurrentHelth { get; private set; }

    private void Start()
    {
        CurrentHelth = m_maxHealthpoints;
    }

    public void TakeDamage()
    {
        CurrentHelth--;
        if (CurrentHelth <= 0)
        {
            Destroy(gameObject);
            m_deathCanvas.transform.localScale = Vector3.zero;
            m_deathCanvas.SetActive(true);
            m_deathCanvas.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
        }
        m_playerHealthbar.UpdateHealthbar();
    }

    public int GetMaxHealthpoints()
        => m_maxHealthpoints;
}
