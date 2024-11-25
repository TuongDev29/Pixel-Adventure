using UnityEngine;

public class PlayerDamageSender : DamageSender
{
    [Header("PlayerDamageSender")]
    [SerializeField] protected PlayerController playerCtrl;
    [SerializeField] protected Vector2 sendDamageForce = new Vector2(2f, 4f);

    protected override void ResetValue()
    {
        base.ResetValue();
        this.damage = playerCtrl.PlayerDataSO.damage;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.AutoLoad<PlayerController>(ref this.playerCtrl, transform.GetComponent<PlayerController>());
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            IDamageable damageable = other.transform.GetComponent<IDamageable>();
            if (damageable == null) return;

            Vector2 direction = (other.transform.position - transform.position).normalized;
            if (Vector2.Dot(direction, Vector2.down) <= 0.84) return;

            this.SendDamage(damageable);
            playerCtrl.rb.velocity = -direction * sendDamageForce;
        }
    }
}