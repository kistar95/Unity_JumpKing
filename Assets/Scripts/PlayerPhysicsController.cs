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

    public void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        _groundLayerMask = LayerMask.GetMask("Ground");
        //_groundCollider.sharedMaterial = _normalMaterial;
        _collider.sharedMaterial = _normalMaterial;

        StartCoroutine(Co_CheckGround());
    }

    private void SetPhysicsMaterial(PhysicsMaterial2D inMaterial)
    {
        if (_collider == null)
        {
            return;
        }

        _collider.sharedMaterial = inMaterial;
    }

    public void SetBounceMaterial()
    {
        SetPhysicsMaterial(_bounceMaterial);
    }

    private bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, PlayerHelper.Instance.GroundCheckLength, _groundLayerMask);

        return hit.collider != null;
    }

    private IEnumerator Co_CheckGround()
    {
        while (true)
        {
            if (CheckGround() == true)
            {
                SetPhysicsMaterial(_normalMaterial);
            }
            else
            {
                SetPhysicsMaterial(_bounceMaterial);
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 점프 시 bounce 메터리얼로 교체, 바닥부분 ray로 바닥을 미리 감지하고 normal 메터리얼로 교체

        if (collision.collider.tag != "Ground")
        {
            return;
        }

        Vector2 _normal = collision.contacts[0].normal;

        //if (_normal.y > 0.5f)
        //{
        //    _collider.sharedMaterial = _normalMaterial;
        //}
        //else
        //{
        //    _collider.sharedMaterial = _bounceMaterial;
        //}

        //Vector2 _direction = (collision.contacts[0].point - (Vector2)_collider.bounds.center).normalized;

        //if (_direction.y < -0.5f && Mathf.Abs(_direction.x) < 0.5f)
        //{
        //    //_groundCollider.sharedMaterial = _normalMaterial;
        //    _collider.sharedMaterial = _normalMaterial;
        //}
        //else
        //{
        //    //_groundCollider.sharedMaterial = _bounceMaterial;
        //    _collider.sharedMaterial = _bounceMaterial;
        //}
    }


}
