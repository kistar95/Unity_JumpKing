using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    /// <summary>
    /// �⺻ ���׸���
    /// </summary>
    [SerializeField] private PhysicsMaterial2D _normalMaterial;

    /// <summary>
    /// ��, �Ʒ� �浹 ���׸���
    /// </summary>
    [SerializeField] private PhysicsMaterial2D _bounceMaterial;
    
    /// <summary>
    /// ���� �ݶ��̴�
    /// </summary>
    [SerializeField] private CompositeCollider2D _groundCollider;

    private Collider2D _collider;
    private LayerMask _groundLayerMask;

    /// <summary>
    /// ĳ���Ͱ� ���� �ִ���
    /// </summary>
    public bool IsGrounded { get; set; }

    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
    }

    public void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        _groundLayerMask = LayerMask.GetMask("Ground");
        //_groundCollider.sharedMaterial = _normalMaterial;
        _collider.sharedMaterial = _normalMaterial;

        //StartCoroutine(Co_CheckGround());
    }

    private void SetPhysicsMaterial(PhysicsMaterial2D inMaterial)
    {
        if (_collider == null)
        {
            return;
        }

        _collider.sharedMaterial = inMaterial;
    }

    private bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, PlayerHelper.Instance.GroundCheckLength, _groundLayerMask);

        return hit.collider != null;
    }

    private void UpdatePhysicsMaterial()
    {
        if (CheckGround() == true)
        {
            //IsGrounded = true;
            SetPhysicsMaterial(_normalMaterial);
        }
        else
        {
            IsGrounded = false;
            SetPhysicsMaterial(_bounceMaterial);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Ground")
        {
            return;
        }

        Vector2 _normal = collision.contacts[0].normal;

        // ��Ȯ�� �ٴ� ������ ���� ����ó���� ���⿡
        if (_normal.y > 0.1f)
        {
            IsGrounded = true;
        }
    }
}
