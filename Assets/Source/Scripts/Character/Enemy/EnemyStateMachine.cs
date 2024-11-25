using System.Collections;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public enum EEnemyState
    {
        Idle,
        Patrol,
        Chase,
        Dead
    }

    protected EnemyController enemyCtrl;

    public EnemyStateMachine(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
        this.enemyCtrl = monoBehaviour as EnemyController;
    }

    protected override void InitializeState()
    {
        //Add State
        this.AddState(EEnemyState.Idle, new EnemyIdleState(enemyCtrl, this));
        this.AddState(EEnemyState.Patrol, new EnemyPatrolState(enemyCtrl, this));
        this.AddState(EEnemyState.Chase, new EnemyChaseState(enemyCtrl, this));
        this.AddState(EEnemyState.Dead, new EnemyDeadState(enemyCtrl, this));

        //Defalt enemy State
        this.ChangeState(EEnemyState.Idle);
    }

    public virtual bool TryDeadState()
    {
        if (this.enemyCtrl.EnemyDamageable.IsDead)
        {
            this.ChangeState(EEnemyState.Dead);
            return true;
        }
        return false;
    }

    public virtual bool TryChaseState()
    {
        if (!enemyCtrl.Context.canChasePlayer) return false;

        if (Vector2.Distance(enemyCtrl.transform.position, PlayerManager.Instance.GetPosition())
            < enemyCtrl.Context.sightRange)
        {
            this.ChangeState(EEnemyState.Chase);
            return true;
        }

        return false;
    }
}
