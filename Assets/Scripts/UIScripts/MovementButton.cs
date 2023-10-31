using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _movementValue;

    private bool _isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }

    public float GetMovementValue()
    {
        if (_isPressed)
            return _movementValue;
        else 
            return 0;
    }
}
