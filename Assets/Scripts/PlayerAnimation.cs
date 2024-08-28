using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using System;
using UnityEngine.TextCore.Text;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMain _playerMain;

    private void Start()
    {
        _playerMain = GetComponent<PlayerMain>();
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
}
