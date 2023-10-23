using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthPoints m_playerHealthpoints;
    private Slider _healthbarSLider;

    private void Awake()
    {
        _healthbarSLider = GetComponent<Slider>();
        _healthbarSLider.maxValue = m_playerHealthpoints.GetMaxHealthpoints();
    }

    public void UpdateHealthbar() 
        => _healthbarSLider.value = m_playerHealthpoints.CurrentHelth;

}
