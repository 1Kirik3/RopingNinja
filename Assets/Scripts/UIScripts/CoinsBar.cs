using TMPro;
using UnityEngine;

public class CoinsBar : MonoBehaviour
{
    [SerializeField] private PlayerInventory m_playerInventory;
    private TMP_Text _coinsCounterText;

    private void Awake()
    {
        _coinsCounterText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        UpdateCounterString();
    }

    private void OnEnable()
    {
        Coin.onCoinCollet += UpdateCounterString;
    }

    private void OnDisable()
    {
        Coin.onCoinCollet -= UpdateCounterString;
    }

    public void UpdateCounterString()
        => _coinsCounterText.SetText(PlayerInventory.coinsCounter.ToString());
}
