using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    /// <summary>
    /// 기본 메테리얼
    /// </summary>
    [SerializeField] private PhysicsMaterial2D _normalMaterial;

    /// <summary>
    /// 옆, 아래 충돌 메테리얼
    /// </summary>
    [SerializeField] private PhysicsMaterial2D _bounceMaterial;
    
    /// <summary>
    /// 지형 콜라이더
    /// </summary>
    [SerializeField] private CompositeCollider2D _groundCollider;

    private Collider2D _collider;
    private LayerMask _groundLayerMask;

    /// <summary>
    /// 캐릭터가 땅에 있는지
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

        // 정확한 바닥 감지를 위해 닿은처리는 여기에
        if (_normal.y > 0.1f)
        {
            IsGrounded = true;
        }
    }
}
