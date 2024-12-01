using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterController2D : MonoBehaviour
{
    private static readonly int LeftRight = Animator.StringToHash("LeftRight");
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] private Animator _animator;

    private CharacterController characterController;

    private FrameInput _frameInput;
    private Vector3 _frameVelocity;
    public Vector2 FrameInput => _frameInput.Move;
    public Vector2 lookDirection;

    private float _time;

    private float depthZ;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        depthZ = transform.localPosition.z;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        lookDirection = _frameVelocity * 5.0f;
        _animator.SetFloat(LeftRight, Mathf.Clamp(lookDirection.x, -1f, 1f));
        GatherInput();
    }

    private void GatherInput()
    {
        _frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.W),
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

        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();

        characterController.Move(_frameVelocity);
        Vector3 pos = transform.position;
        pos.z = depthZ;
        transform.position = pos;
    }


    #region Collisions

    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded;

    private void CheckCollisions()
    {
        // Ground and Ceiling
        bool groundHit = characterController.isGrounded;
        bool ceilingHit =
            Physics.CapsuleCast(characterController.bounds.center + Vector3.down * (characterController.height / 2.0f + 0.1f),
                characterController.bounds.center + Vector3.up * (characterController.height / 2.0f - 0.1f),
                characterController.radius, Vector3.up, _stats.GrounderDistance, LayerMask.GetMask("Ground"));

        Debug.DrawLine(characterController.bounds.center + Vector3.up * (characterController.height / 2.0f + _stats.GrounderDistance),
            characterController.bounds.center + Vector3.down * (characterController.height / 2.0f - _stats.GrounderDistance), Color.red,
            Time.deltaTime, false);

        // Hit a Ceiling
        if (ceilingHit)
        {
            _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);
        }

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
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


    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && characterController.velocity.y > 0)
            _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _stats.JumpPower;
        GlobalManager.Instance.Jumped?.Invoke();
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
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
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

public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
}