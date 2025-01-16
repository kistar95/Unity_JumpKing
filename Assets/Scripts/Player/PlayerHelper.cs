using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : SingletonMonoBehaviour<PlayerHelper>
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float MoveSpeed = 4;

    /// <summary>
    /// 점프력
    /// </summary>
    public float JumpForce = 8;

    /// <summary>
    /// 점프 딜레이
    /// </summary>
    public float JumpDelay = 0.2f;

    /// <summary>
    /// 최대 점프 차징 시간
    /// </summary>
    public float HoldTime = 2.0f;

    /// <summary>
    /// 바닥 인지 길이
    /// </summary>
    public float GroundCheckLength = 0.2f;
}
