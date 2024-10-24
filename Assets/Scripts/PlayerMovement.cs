using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    // SpriteRenderer �� �ڽ� ������Ʈ�� �ֱ⿡ SerializeField ���
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerInput _playerInput; // SendMessages ����� �ǵ��� �����ʴ����� ����

    private PlayerAnimation _playerAnimation;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private EPlayerState _playerState = EPlayerState.IDLE;

    /// <summary>
    /// ĳ���Ͱ� ���� ��Ҵ���
    /// </summary>
    private bool _isGrounded;

    /// <summary>
    /// ���� �غ�������
    /// </summary>
    private bool _isCharging;

    /// <summary>
    /// ��, �� ����
    /// </summary>
    private float _direction = 1.0f;

    /// <summary>
    /// �̵� �׼�
    /// </summary>
    private InputAction _moveAction;

    /// <summary>
    /// ���� �׼�
    /// </summary>
    private InputAction _jumpAction;

    public void Initialize(PlayerAnimation inPlayerAnimation)
    {
        _playerAnimation = inPlayerAnimation;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        ChangeState(EPlayerState.IDLE);

        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];

        _moveAction.performed += OnMove; // ������ ��
        _moveAction.canceled += OnStop; // ���� ��
        // �ӽ�
        _isGrounded = true;
        _isCharging = false;
    }

    private void FixedUpdate()
    {
        // �̵�, ������¡, ������ ����
        //Move();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input != null)
        {
            _rigidbody.velocity = new Vector2(input.x * PlayerHelper.Instance.MoveSpeed, input.y);
            ChangeState(EPlayerState.WALK);
            Turn(input.x);
        }
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        _rigidbody.velocity = Vector2.zero;
        ChangeState(EPlayerState.IDLE);
    }

    private void OnJumpCharge(InputAction.CallbackContext context)
    {
        //_rigidbody.velocity = new Vector2(0, PlayerHelper.Instance.JumpForce);
        //_playerAnimation.Jump();
        Debug.Log("jump");
    }

    private void OnJump(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// ���� ��ȯ
    /// </summary>
    /// <param name="direction"></param>
    private void Turn(float inDirection)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(inDirection) * Mathf.Abs(scale.x);
        transform.localScale = scale;

        _direction = scale.x;
    }

    private IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(PlayerHelper.Instance.JumpDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.1f)
        {
            _isGrounded = true;
        }
    }

    private void ChangeState(EPlayerState inState)
    {
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
        }
    }

    private void Move()
    {
        if (_isGrounded == false || _isCharging == true)
        {
            return;
        }

        // ��, �� �Է��� ���� ��
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _rigidbody.velocity = Vector2.zero; // �����ȵ�
            _playerAnimation.Idle();
            return;
        }

        float direction = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(direction * PlayerHelper.Instance.MoveSpeed, _rigidbody.velocity.y);

        if (Input.GetButton("Horizontal"))
        {
            Turn(_rigidbody.velocity.x);
        }

        if (direction != 0)
        {
            _playerAnimation.Run();
        }
        else
        {
            _playerAnimation.Idle();
        }
    }

    private void JumpCharging()
    {
        if (Input.GetButton("Jump"))
        {
            _rigidbody.velocity = Vector2.zero;
            _isCharging = true;
            _playerAnimation.Ready();
        }

        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonUp("Jump"))
        {
            _rigidbody.velocity = new Vector2(0, PlayerHelper.Instance.JumpForce);
            _playerAnimation.Jump();
        }
    }

    
}
