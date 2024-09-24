using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private PlayerAnimation _playerAnimation;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private EPlayerState _playerState = EPlayerState.IDLE;

    /// <summary>
    /// 캐릭터가 땅에 닿았는지
    /// </summary>
    private bool _isGrounded;

    /// <summary>
    /// 점프 준비중인지
    /// </summary>
    private bool _isCharged;

    /// <summary>
    /// 좌, 우 방향
    /// </summary>
    private float _direction;

    /// <summary>
    /// 이동속도
    /// </summary>
    private float _moveSpeed = 5;

    public void Initialize(PlayerAnimation inPlayerAnimation)
    {
        _playerAnimation = inPlayerAnimation;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        // 임시
        _isCharged = false;
        _isCharged = true;
    }

    private void FixedUpdate()
    {
        // Ready -> Jump
        Move();
    }

    private void Move()
    {
        // 기존 inputSystem
        if (_isCharged == false && _isGrounded == true)
        {
            _direction = Input.GetAxisRaw("Horizontal");
            _rigidbody.velocity = new Vector2(_direction * _moveSpeed, _rigidbody.velocity.y);

            if (Input.GetButton("Horizontal"))
            {
                _sprite.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            _playerAnimation.Walk();
        }
    }

    private void Ready()
    {
        _playerAnimation.Ready();

        Jump();
    }

    private void Jump()
    {
        _playerAnimation.Jump();
    }
}
