using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static int coinsCounter = 0;

    private void OnEnable()
    {
        Coin.onCoinCollet += IncreaseCoins;
    }

    private void OnDisable()
    {
        Coin.onCoinCollet -= IncreaseCoins;
    }

    public void IncreaseCoins()
        => coinsCounter++;
}
