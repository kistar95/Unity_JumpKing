using Assets.PixelFantasy.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : Creature // 필요한가?
{
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerPhysicsController _playerPhysicsController;

    private void Start()
    {
        _playerAnimation.Initialize(this);
        _playerMovement.Initialize(_playerAnimation, _playerPhysicsController);
        _playerPhysicsController.Initialize();
    }
}
