using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundedCheck : GroundedCheck<PlayerGroundedCheck>, ICheckCollisionDown
{
    protected PlayerController playerCtrl;
    [SerializeField] protected float distanceChecking = 0.42f;
    protected Vector2 sizeCollider;

    public PlayerGroundedCheck(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
        playerCtrl = monoBehaviour as PlayerController;
        this.sizeCollider = (playerCtrl.Collider2D as CapsuleCollider2D).size;

        GizmosDrawer.Instance.AddDrawAction(() =>
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(playerCtrl.transform.position, Vector2.down * distanceChecking);
        });
    }

    public bool CheckCollisionDown()
    {
        RaycastHit2D hit = Physics2D.CapsuleCast(playerCtrl.transform.position, sizeCollider,
                CapsuleDirection2D.Vertical, 0, Vector2.down, distanceChecking - (sizeCollider.y / 2));

        return hit.collider != null && !hit.collider.isTrigger;
    }
}
