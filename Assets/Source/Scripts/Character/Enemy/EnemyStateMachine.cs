using UnityEngine;

public abstract class EnemyStateMachine : StateMachine
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

    public virtual bool TryIdleState()
    {
        if (!this.ContainState(EEnemyState.Idle)) return false;

        if (this.CompareState(EEnemyState.Patrol))
        {
            this.ChangeState(EEnemyState.Idle, enemyCtrl.EnemyData.patrolWaitTime);
        }
        return true;
    }

    public virtual bool TryDeadState()
    {
        if (!this.ContainState(EEnemyState.Dead)) return false;

        if (this.enemyCtrl.EnemyDamageable.IsDead)
        {
            this.ChangeState(EEnemyState.Dead);
            return true;
        }
        return false;
    }

    public bool TryPatrolState()
    {
        if (!this.ContainState(EEnemyState.Patrol)) return false;
        this.ChangeState(EEnemyState.Patrol);

        return true;
    }

    public bool TryChaseState()
    {
        if (!this.ContainState(EEnemyState.Chase)) return false;

        if (Vector2.Distance(enemyCtrl.transform.position, PlayerManager.Instance.GetPosition())
            < enemyCtrl.EnemyData.detectionRange)
        {
            this.ChangeState(EEnemyState.Chase);
            return true;
        }

        return false;
    }
}
