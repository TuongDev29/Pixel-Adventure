using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : CharacterController
{
    [SerializeField] private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;
    [SerializeField] private Rigidbody2D _rb;
    public Rigidbody2D rb => _rb;
    [SerializeField] private EnemyDamageable _enemyDamageable;
    public EnemyDamageable EnemyDamageable => _enemyDamageable;
    [SerializeField] private EnemyAI _enemyAI;
    public EnemyAI EnemyAI => _enemyAI;
    [SerializeField] private Directional _directional;
    public Directional Directional => _directional;
    protected EnemyStateMachine enemyStateMachine;

    protected virtual void Start()
    {
        this._directional = new EnemyDirectional(this);
        this.enemyStateMachine = InitEnemyStateMachine();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody2D();

        this.AutoLoad<EnemyAI>(ref this._enemyAI, GetComponent<EnemyAI>());
        this.AutoLoad<EnemyDamageable>(ref this._enemyDamageable, GetComponent<EnemyDamageable>());
    }

    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = transform.GetComponent<Rigidbody2D>();
        this._rb.angularDrag = 10;
        Debug.LogWarning("LoadRigidbody2D", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.enemyStateMachine?.ExcuteState();
    }

    protected abstract EnemyStateMachine InitEnemyStateMachine();
}
