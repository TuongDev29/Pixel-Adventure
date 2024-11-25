using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;
    [SerializeField] private Rigidbody2D _rb;
    public Rigidbody2D rb => _rb;
    [SerializeField] private EnemyDamageable _enemyDamageable;
    public EnemyDamageable EnemyDamageable => _enemyDamageable;
    [SerializeField] private EnemyContext _context;
    public EnemyContext Context => _context;
    [SerializeField] private Directional _directional;
    public Directional Directional => _directional;
    private EnemyStateMachine _enemyStateMachine;

    protected virtual void Start()
    {
        this._directional = new EnemyDirectional(this);
        this._enemyStateMachine = new EnemyStateMachine(this);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody2D();

        this.AutoLoad<EnemyContext>(ref this._context, GetComponent<EnemyContext>());
        this.AutoLoad<EnemyDamageable>(ref this._enemyDamageable, GetComponent<EnemyDamageable>());
    }

    protected virtual void Update()
    {
        this._enemyStateMachine.UpdateState();
    }

    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = transform.GetComponent<Rigidbody2D>();
        this._rb.angularDrag = 10;
        Debug.LogWarning("LoadRigidbody2D", gameObject);
    }
}
