using System.Collections;
using UnityEngine;

public class ChickenStateMachine : EnemyStateMachine
{
    public ChickenStateMachine(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
    }

    protected override void InitializeState()
    {
        this.AddState(EEnemyState.Idle, new EnemyIdleState(enemyCtrl, this));
        this.AddState(EEnemyState.Chase, new EnemyChaseState(enemyCtrl, this));
        this.AddState(EEnemyState.Dead, new EnemyDeadState(enemyCtrl, this));

        this.ChangeState(EEnemyState.Idle);
    }
}