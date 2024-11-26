using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    [Header("PlayerDamageable")]
    [SerializeField] protected PlayerController playerCtrl;
    [SerializeField] protected float knockBackForce = 4f;
    [SerializeField] private Vector2 upwardForce;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.AutoLoad<PlayerController>(ref this.playerCtrl, transform.GetComponent<PlayerController>());
    }

    protected override void ResetValue()
    {
        base.ResetValue();

        this.maxHealth = playerCtrl.PlayerDataSO.maxHealth;
    }

    protected override void OnReceiverDamage()
    {
        Vector2 direction = (transform.position - senderPosition).normalized;
        playerCtrl.rb.velocity = direction * knockBackForce;
        playerCtrl.PlayerState.ChangeState(EPlayerState.Hurt);
    }

    protected override void OnDead()
    {
        playerCtrl.Collider2D.isTrigger = true;
        playerCtrl.rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
        playerCtrl.rb.freezeRotation = false;
        playerCtrl.rb.AddTorque(2f, ForceMode2D.Impulse);
        playerCtrl.PlayerState.ChangeState(EPlayerState.Dead);
    }

    protected override void OnHealing()
    {
    }
}
