using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : SingletonMonoBehaviour<PlayerHelper>
{
    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float MoveSpeed = 4;

    /// <summary>
    /// ������
    /// </summary>
    public float JumpForce = 8;

    /// <summary>
    /// ���� ������
    /// </summary>
    public float JumpDelay = 0.2f;

    /// <summary>
    /// �ִ� ���� ��¡ �ð�
    /// </summary>
    public float HoldTime = 2.0f;

    /// <summary>
    /// �ٴ� ���� ����
    /// </summary>
    public float GroundCheckLength = 0.2f;
}
