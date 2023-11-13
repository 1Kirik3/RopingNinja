using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    public int skinCost;
    public int skinNumber;
    public Sprite skinSprite;

    private void Awake()
    {
        skinSprite = GetComponent<Image>()?.sprite;
    }
}
