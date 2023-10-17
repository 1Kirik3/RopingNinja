using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private RopeSystem _playerRope;

    private void Awake()
    {
        _playerRope = GetComponent<RopeSystem>();
    }

    private void FixedUpdate()
    {
        _playerRope.ApplySwingingForce();
        _playerRope.ClampRopeMovement();
    }

}
