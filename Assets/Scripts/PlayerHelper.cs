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
    public float JumpForce = 5;

    /// <summary>
    /// 점프 딜레이
    /// </summary>
    public float JumpDelay = 0.2f;
}
