using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    // SpriteRenderer 가 자식 오브젝트에 있기에 SerializeField 사용
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerInput _playerInput; // SendMessages 방식은 되도록 쓰지않는편이 좋다

    private PlayerAnimation _playerAnimation;
    private PlayerPhysicsController _playerPhysicsController;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private EPlayerState _playerState = EPlayerState.IDLE;

    /// <summary>
    /// 이동 액션
    /// </summary>
    private InputAction _moveAction;

    /// <summary>
    /// 점프 액션
    /// </summary>
    private InputAction _jumpAction;

    /// <summary>
    /// 캐릭터가 땅에 닿았는지
    /// </summary>
    private bool _isGrounded = false;

    /// <summary>
    /// 점프 준비중인지
    /// </summary>
    private bool _isCharging = false;

    /// <summary>
    /// 풀 차징 점프인지
    /// </summary>
    private bool _isChargeMax = false;

    /// <summary>
    /// 현재 점프력
    /// </summary>
    private float _currentJumpForce = 1.0f;

    /// <summary>
    /// 좌, 우 방향
    /// </summary>
    private float _jumpDirection = 0;

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="inPlayerAnimation"></param>
    /// <param name="inPlayerPhysicsController"></param>
    public void Initialize(PlayerAnimation inPlayerAnimation, PlayerPhysicsController inPlayerPhysicsController)
    {
        _playerAnimation = inPlayerAnimation;
        _playerPhysicsController = inPlayerPhysicsController;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        ChangeState(EPlayerState.IDLE);

        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];

        _moveAction.performed += OnMove; // 눌렀을 때
        _moveAction.canceled += OnStop; // 땠을 때

        _jumpAction.started += OnJumpReady;
        _jumpAction.performed += OnJumpCharge;
        _jumpAction.canceled += OnJump;

        // 임시
        //_isGrounded = true;
        //_isCharging = false;
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnStop;

        _jumpAction.started -= OnJumpReady;
        _jumpAction.performed -= OnJumpCharge;
        _jumpAction.canceled -= OnJump;
    }

    private void Update()
    {
        // 점프 차징
        if (_isCharging == true)
        {
            _currentJumpForce += Time.deltaTime;
        }
        // 바닥 체크(낙하 상황 등), IsGrounded가 false 가되면 ChangeState
        if (_playerPhysicsController.IsGrounded == false)
        {
            ChangeState(EPlayerState.FALL);
        }

        SetJumpDirection();
    }

    /// <summary>
    /// 캐릭터 이동(좌,우 방향키 입력 시)
    /// </summary>
    /// <param name="context"></param>
    private void OnMove(InputAction.CallbackContext context)
    {
        if (_playerPhysicsController.IsGrounded == false || _isCharging == true)
        {
            return;
        }
        //if (_isGrounded == false || _isCharging == true)
        //{
        //    return;
        //}

        Vector2 input = context.ReadValue<Vector2>();

        if (input != null)
        {
            _rigidbody.linearVelocity = new Vector2(input.x * PlayerHelper.Instance.MoveSpeed, input.y);
            ChangeState(EPlayerState.WALK);
            Turn(input.x);
        }
    }

    /// <summary>
    /// 캐릭터 정지(좌, 우 방향키 눌렀다 땟을 때)
    /// </summary>
    /// <param name="context"></param>
    private void OnStop(InputAction.CallbackContext context)
    {
        if (_playerPhysicsController.IsGrounded == false || _isCharging == true)
        {
            return;
        }
        //if (_isGrounded == false || _isCharging == true)
        //{
        //    return;
        //}

        _rigidbody.linearVelocity = Vector2.zero;
        ChangeState(EPlayerState.IDLE);
    }

    // 점프 로직
    // started -> 점프 버튼 눌렀을 때(차징 시작)
    // performed -> 점프 버튼을 누른 후 hold time이 지났을 때(풀차징 점프)
    // canceled -> 점프 버튼을 누른 후 hold time이 지나기전에 버튼을 땠을 때(점프)

    /// <summary>
    /// 점프 준비(점프 버튼을 눌렀을 때) -> started
    /// </summary>
    /// <param name="context"></param>
    private void OnJumpReady(InputAction.CallbackContext context)
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _isCharging = true;
        ChangeState(EPlayerState.CHARGE);
        Debug.Log("jump ready");
    }

    /// <summary>
    /// 풀차징 점프(hold time이 지났을 때) -> performed
    /// </summary>
    /// <param name="context"></param>
    private void OnJumpCharge(InputAction.CallbackContext context)
    {
        _rigidbody.linearVelocity = new Vector2(PlayerHelper.Instance.JumpForce * _currentJumpForce * _jumpDirection / 2, PlayerHelper.Instance.JumpForce * _currentJumpForce);
        ChangeState(EPlayerState.JUMP);
        //Debug.Log(_currentJumpForce);

        _isChargeMax = true;
        _isCharging = false;
        _isGrounded = false; // 나중에 삭제
        //_playerPhysicsController.IsGrounded = false;
        _currentJumpForce = 1.0f;
        //Debug.Log("jump max");
    }

    /// <summary>
    /// 점프(점프 버튼을 땠을 때) -> canceled
    /// </summary>
    /// <param name="context"></param>
    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isChargeMax == true)
        {
            _isChargeMax = false;
            return;
        }

        _rigidbody.linearVelocity = new Vector2(PlayerHelper.Instance.JumpForce * _currentJumpForce * _jumpDirection / 2, PlayerHelper.Instance.JumpForce * _currentJumpForce);
        ChangeState(EPlayerState.JUMP);
        //Debug.Log(_currentJumpForce);

        _isCharging = false;
        _isGrounded = false; // 나중에 삭제
        //_playerPhysicsController.IsGrounded = false;
        _currentJumpForce = 1.0f;
        //Debug.Log("jump");
    }

    /// <summary>
    /// 점프 이동방향 결정
    /// </summary>
    private void SetJumpDirection()
    {
        if (_isCharging == false)
        {
            return;
        }

        float _direction = Input.GetAxisRaw("Horizontal");

        _jumpDirection = _direction;
        Turn(_direction);
    }

    /// <summary>
    /// 방향 전환
    /// </summary>
    /// <param name="direction"></param>
    private void Turn(float inDirection)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(inDirection) * Mathf.Abs(scale.x);
        transform.localScale = scale;

        //_direction = scale.x;
    }

    private IEnumerator Co_JumpDelay()
    {
        yield return new WaitForSeconds(PlayerHelper.Instance.JumpDelay);
    }

    private void ChangeState(EPlayerState inState)
    {
        if (_playerState == inState)
        {
            return;
        }

        _playerState = inState;

        switch (_playerState)
        {
            case EPlayerState.IDLE:
                _playerAnimation.Idle();
                break;
            case EPlayerState.WALK:
                _playerAnimation.Run();
                break;
            case EPlayerState.CHARGE:
                _playerAnimation.Ready();
                break;
            case EPlayerState.JUMP:
                _playerAnimation.Jump();
                break;
            case EPlayerState.FALL:
                _playerAnimation.Fall();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 _normal = collision.contacts[0].normal;

        if (_normal.y > 0.1f)
        {
            _isGrounded = true; // 나중에 삭제
            _rigidbody.linearVelocity = Vector2.zero;

            ChangeState(EPlayerState.IDLE);
        }
    }
}
