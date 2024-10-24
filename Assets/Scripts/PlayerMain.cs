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
        _playerAnimation.Initialize(this);
        _playerMovement.Initialize(_playerAnimation);
    }

    private void Update()
    {
        
    }
}
