using Assets.PixelFantasy.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : Creature // �ʿ��Ѱ�?
{
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement.Initialize(_playerAnimation);
        _playerAnimation.Initialize(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnimation.Jump();
        }
    }
}
