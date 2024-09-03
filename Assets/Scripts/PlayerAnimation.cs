using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using System;
using UnityEngine.TextCore.Text;
using Assets.PixelFantasy.Common.Scripts;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMain _playerMain;

    public void Initialize(PlayerMain inPlayerMain)
    {
        _playerMain = inPlayerMain;
    }

    public void Idle()
    {
        SetState(CharacterState.Idle);
    }

    public void Ready()
    {
        SetState(CharacterState.Ready);
    }

    public void Jump()
    {
        SetState(CharacterState.Jump);
        EffectManager.Instance.CreateSpriteEffect(_playerMain, "Jump");
    }

    public void SetState(CharacterState inState)
    {
        foreach (var variable in new[] { "Idle", "Ready", "Walk", "Run", "Crouch", "Crawl", "Jump", "Fall", "Land", "Block", "Climb", "Die" })
        {
            _playerMain.Animator.SetBool(variable, false);
        }

        switch (inState)
        {
            case CharacterState.Idle: _playerMain.Animator.SetBool("Idle", true); break;
            case CharacterState.Ready: _playerMain.Animator.SetBool("Ready", true); break;
            case CharacterState.Walk: _playerMain.Animator.SetBool("Walk", true); break;
            case CharacterState.Run: _playerMain.Animator.SetBool("Run", true); break;
            case CharacterState.Crouch: _playerMain.Animator.SetBool("Crouch", true); break;
            case CharacterState.Crawl: _playerMain.Animator.SetBool("Crawl", true); break;
            case CharacterState.Jump: _playerMain.Animator.SetBool("Jump", true); break;
            case CharacterState.Fall: _playerMain.Animator.SetBool("Fall", true); break;
            case CharacterState.Land: _playerMain.Animator.SetBool("Land", true); break;
            case CharacterState.Block: _playerMain.Animator.SetBool("Block", true); break;
            case CharacterState.Climb: _playerMain.Animator.SetBool("Climb", true); break;
            case CharacterState.Die: _playerMain.Animator.SetBool("Die", true); break;
            default: throw new NotSupportedException(inState.ToString());
        }
    }

    public CharacterState GetState()
    {
        if (_playerMain.Animator.GetBool("Idle")) return CharacterState.Idle;
        if (_playerMain.Animator.GetBool("Ready")) return CharacterState.Ready;
        if (_playerMain.Animator.GetBool("Walk")) return CharacterState.Walk;
        if (_playerMain.Animator.GetBool("Run")) return CharacterState.Run;
        if (_playerMain.Animator.GetBool("Crawl")) return CharacterState.Crawl;
        if (_playerMain.Animator.GetBool("Crouch")) return CharacterState.Crouch;
        if (_playerMain.Animator.GetBool("Jump")) return CharacterState.Jump;
        if (_playerMain.Animator.GetBool("Fall")) return CharacterState.Fall;
        if (_playerMain.Animator.GetBool("Land")) return CharacterState.Land;
        if (_playerMain.Animator.GetBool("Block")) return CharacterState.Block;
        if (_playerMain.Animator.GetBool("Climb")) return CharacterState.Climb;
        if (_playerMain.Animator.GetBool("Die")) return CharacterState.Die;

        return CharacterState.Ready;
    }
}
