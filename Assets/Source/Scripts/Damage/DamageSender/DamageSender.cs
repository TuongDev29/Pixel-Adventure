using UnityEngine;

public class DamageSender : MainMonoBehaviour, IDamageSender
{
    [SerializeField] protected int damage = 1;

    public void SendDamage(IDamageable damageable)
    {
        damageable.Receiver(damage, transform.position);
    }
}
