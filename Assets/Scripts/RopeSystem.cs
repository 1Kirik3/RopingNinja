using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RopeSystem : MonoBehaviour
{
    private bool _ropeAttached;
    private Rigidbody2D _ropeHingeAnchorRb;
    private SpriteRenderer _ropeHingeAnchorSprite;
    private List<Vector2> _ropePositions = new List<Vector2>();
    private Vector2 _ropeHook;
    private bool _distanceSet;
    public bool isSwinging;

    private Vector2 _playerPosition;
    private Rigidbody2D _playerRB;
    private SpriteRenderer _playerSprite;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isColliding;

    [SerializeField] private LineRenderer _ropeRenderer;
    [SerializeField] private LayerMask _ropeLayerMask;
    [SerializeField] private GameObject _ropeHingeAnchor;
    [SerializeField] private DistanceJoint2D _ropeJoint;
    [SerializeField] private float _ropeMaxCastDistance;
    [SerializeField] private float _climbSpeed;
    [SerializeField] private float _swingForce;
    [SerializeField] private float _maxRopeSize;
    [SerializeField] private float _minRopeSize;
    [SerializeField] private Vector2 _maxSwingSpeed;

    private void Awake()
    {
        _ropeJoint.enabled = false;
        _playerPosition = transform.position;
        _playerRB = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _ropeHingeAnchorRb = _ropeHingeAnchor.GetComponent<Rigidbody2D>();
        _ropeHingeAnchorSprite = _ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInputVector();

        var facingDirection = _ropeHingeAnchor.transform.position - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        _playerPosition = transform.position;

        if (!_ropeAttached)
        {
            isSwinging = false;
        }
        else
        {
            isSwinging = true;
            _ropeHook = _ropePositions.Last();
        }

        HandleInput(aimDirection);

        UpdateRopePositions();

        HandleRopeLength();

    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (_ropeAttached) return;
        _ropeRenderer.enabled = true;

        var hit = Physics2D.Raycast(_playerPosition, aimDirection, _ropeMaxCastDistance, _ropeLayerMask);

        if (hit.collider != null)
        {
            _ropeAttached = true;
            if (!_ropePositions.Contains(hit.point))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                _ropePositions.Add(hit.point);
                _ropeJoint.distance = Vector2.Distance(_playerPosition, hit.point);
                _ropeJoint.enabled = true;
                _ropeHingeAnchorSprite.enabled = true;
            }
        }

        else
        {
            _ropeRenderer.enabled = false;
            _ropeAttached = false;
            _ropeJoint.enabled = false;
        }
    }

    private void UpdateRopePositions()
    {
        // 1
        if (!_ropeAttached)
        {
            return;
        }

        // 2
        _ropeRenderer.positionCount = _ropePositions.Count + 1;

        // 3
        for (var i = _ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != _ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                _ropeRenderer.SetPosition(i, _ropePositions[i]);

                // 4
                if (i == _ropePositions.Count - 1 || _ropePositions.Count == 1)
                {
                    var ropePosition = _ropePositions[_ropePositions.Count - 1];
                    if (_ropePositions.Count == 1)
                    {
                        _ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!_distanceSet)
                        {
                            _ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            _distanceSet = true;
                        }
                    }
                    else
                    {
                        _ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!_distanceSet)
                        {
                            _ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            _distanceSet = true;
                        }
                    }
                }
                // 5
                else if (i - 1 == _ropePositions.IndexOf(_ropePositions.Last()))
                {
                    var ropePosition = _ropePositions.Last();
                    _ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!_distanceSet)
                    {
                        _ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        _distanceSet = true;
                    }
                }
            }
            else
            {
                // 6
                _ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }

    private void HandleRopeLength()
    {
        if (_ropeAttached && (_verticalInput != 0) && _isColliding == false)
        {
            _ropeJoint.distance -= _verticalInput * _climbSpeed * Time.deltaTime;
            _ropeJoint.distance = Mathf.Clamp(_ropeJoint.distance, _minRopeSize, _maxRopeSize);
        }
    }

    public void GetInputVector()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void ApplySwingingForce()
    {
        if (_horizontalInput != 0)
        {
            //_playerSprite.flipX = _horizontalInput > 0;

            if (isSwinging)
            {
                if (transform.position.y > _ropeHingeAnchor.transform.position.y)
                    _horizontalInput *= -1;

                var playerToHookDirection = (_ropeHook - (Vector2)transform.position).normalized;

                Vector2 perpendicularDirection;
                if (_horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                }

                var force = perpendicularDirection * _swingForce;
                
                _playerRB.AddForce(force, ForceMode2D.Force);
            }
        }
    }

    public void ClampRopeMovement()
    {
        float xClamp = Mathf.Clamp(_playerRB.velocity.x, -_maxSwingSpeed.x, _maxSwingSpeed.x);
        float yClamp = Mathf.Clamp(_playerRB.velocity.y, -_maxSwingSpeed.y, _maxSwingSpeed.y);
        _playerRB.velocity = new Vector2(xClamp, yClamp);

    }

}