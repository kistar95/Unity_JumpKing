using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    // SpriteRenderer �� �ڽ� ������Ʈ�� �ֱ⿡ SerializeField ���
    [SerializeField] private SpriteRenderer _sprite;

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
    private bool _isCharged;

    /// <summary>
    /// ��, �� ����
    /// </summary>
    private float _direction = 1.0f;

    public void Initialize(PlayerAnimation inPlayerAnimation)
    {
        _playerAnimation = inPlayerAnimation;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        // �ӽ�
        _isCharged = false;
        _isGrounded = true;
    }

    private void FixedUpdate()
    {
        // Ready -> Jump
        Move();
        //ReadyToJump();
    }

    private void Move()
    {
        // ��, �� �Է��� ���� ��
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _rigidbody.velocity = Vector2.zero;
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


        //if (_isCharged == false && _isGrounded == true)
        //{

        //}
    }

    private void ReadyToJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _playerAnimation.Ready();
        }
        if (Input.GetButton("Jump"))
        {
            _rigidbody.velocity = Vector2.zero;
            _isCharged = true;
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

    private void ChangeState()
    {
        switch (_playerState)
        {
            case EPlayerState.IDLE:
                break;
            case EPlayerState.WALK:
                break;
            case EPlayerState.CHARGE:
                break;
            case EPlayerState.JUMP:
                break;
        }
    }

   
}
