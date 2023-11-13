using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Skin skin;

    private void Start()
    {
        DeleteButtons();
    }

    public void BuySkin()
    {
        if (PlayerInventory.Instance.CoinsCounter >= skin.skinCost)
        {
            PlayerInventory.Instance.CoinsCounter -= skin.skinCost;
            PlayerInventory.Instance.AddSkin(skin);
            Destroy(gameObject);
        }
        else
        {
            ErrorMessage();
        }
    }

    private void ErrorMessage()
    {
        Debug.Log("Not enough money");
    }

    private void DeleteButtons()
    {
        if (PlayerInventory.Instance.Skins == null)
            return;

        foreach (var item in PlayerInventory.Instance.Skins)
        {
            if (skin.skinNumber == item.skinNumber)
                Destroy(gameObject);
        }
    }
}
