using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public SpriteRenderer _currentSkin;

    private void Awake()
    {
        _currentSkin = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentSkin.sprite = PlayerInventory.Instance.CurrentSkin.skinSprite;
    }
}
