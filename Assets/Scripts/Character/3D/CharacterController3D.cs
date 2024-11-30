using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterController3D : MonoBehaviour
{
    private static readonly int LeftRight = Animator.StringToHash("LeftRight");
    private static readonly int UpDown = Animator.StringToHash("UpDown");
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] private Animator _animator;

    private CharacterController characterController;

    private FrameInput _frameInput;
    private Vector3 _frameVelocity;
    public Vector2 FrameInput => _frameInput.Move;

    private float _time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        
        _animator.SetFloat(LeftRight, Mathf.Clamp(_frameVelocity.x * 5f, -1f, 1f));
        _animator.SetFloat(UpDown, Mathf.Clamp(_frameVelocity.z * 5f, -1f, 1f));
        GatherInput();
    }

    private void GatherInput()
    {
        _frameInput = new FrameInput
        {
            JumpDown = false,
            JumpHeld = false,
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        if (_stats.SnapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold
                ? 0
                : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold
                ? 0
                : Mathf.Sign(_frameInput.Move.y);
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleDirection();
        HandleGravity();

        characterController.Move(_frameVelocity);
    }


    #region Collisions

    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = characterController.isGrounded;
        bool ceilingHit =
            Physics.CapsuleCast(characterController.bounds.center + Vector3.up * (characterController.height / 2.0f),
                characterController.bounds.center + Vector3.down * (characterController.height / 2.0f),
                characterController.radius, Vector3.up, _stats.GrounderDistance, LayerMask.GetMask("Ground"));

        // Hit a Ceiling
        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            GlobalManager.Instance.GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            GlobalManager.Instance.GroundedChanged?.Invoke(false, 0);
        }
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (_frameInput.Move.x == 0)
        {
            var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed,
                _stats.Acceleration * Time.fixedDeltaTime);
        }
        
        if (_frameInput.Move.y == 0)
        {
            var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.z = Mathf.MoveTowards(_frameVelocity.z, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.z = Mathf.MoveTowards(_frameVelocity.z, _frameInput.Move.y * _stats.MaxSpeed,
                _stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = _stats.GroundingForce;
        }
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            _frameVelocity.y =
                Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_stats == null)
            Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
#endif
}