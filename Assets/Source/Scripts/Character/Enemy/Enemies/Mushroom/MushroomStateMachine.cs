using System.Collections;
using UnityEngine;

public class MushroomStateMachine : EnemyStateMachine
{
    public MushroomStateMachine(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
    }

    protected override void InitializeState()
    {
        this.AddState(EEnemyState.Idle, new EnemyIdleState(enemyCtrl, this));
        this.AddState(EEnemyState.Patrol, new EnemyPatrolState(enemyCtrl, this));
        this.AddState(EEnemyState.Dead, new EnemyDeadState(enemyCtrl, this));

        this.ChangeState(EEnemyState.Idle);
    }
}