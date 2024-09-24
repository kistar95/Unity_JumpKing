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
    private float _direction;

    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    private float _moveSpeed = 5;

    public void Initialize(PlayerAnimation inPlayerAnimation)
    {
        _playerAnimation = inPlayerAnimation;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        // �ӽ�
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
        // ���� inputSystem
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
