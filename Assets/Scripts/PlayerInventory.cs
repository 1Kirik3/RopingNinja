using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public int CoinsCounter;
    public List<Skin> Skins = new List<Skin>();
    public Skin CurrentSkin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        Coin.onCoinCollet += IncreaseCoins;
    }

    private void OnDisable()
    {
        Coin.onCoinCollet -= IncreaseCoins;
    }

    public void IncreaseCoins()
        => CoinsCounter++;

    public void AddSkin(Skin skin)
    {
        Skin newSkin = gameObject.AddComponent<Skin>();
        newSkin.skinCost = skin.skinCost;
        newSkin.skinNumber = skin.skinNumber;
        newSkin.skinSprite = skin.skinSprite;
        Skins.Add(newSkin);
    }

    public void SetSkin(Skin skin)
        => CurrentSkin = skin;

}
