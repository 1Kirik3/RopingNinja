using UnityEngine;

public class UseScript : MonoBehaviour
{
    [SerializeField] private Skin skin;

    public void SetSkin()
    {
        foreach (var item in PlayerInventory.Instance.Skins)
        {
            if (skin.skinNumber == item.skinNumber)
                PlayerInventory.Instance.SetSkin(item);
        }
    }
}
