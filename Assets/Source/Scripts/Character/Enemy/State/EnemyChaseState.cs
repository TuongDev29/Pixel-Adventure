using UnityEngine;

public class EnemyChaseState : IState
{
    private EnemyController enemyCtrl;
    private EnemyStateMachine enemyState;

    public EnemyChaseState(EnemyController _enemy, EnemyStateMachine _state)
    {
        this.enemyCtrl = _enemy;
        this.enemyState = _state;
    }

    public void Enter()
    {
        this.enemyCtrl.anim.SetBool("isRunning", true);
    }

    public void Exit()
    {
        this.enemyCtrl.anim.SetBool("isRunning", false);
    }

    public void Excute()
    {
        if (enemyState.TryDeadState()) return;

        bool detectTarget = enemyCtrl.EnemyAI.ChasePlayer();

        if (!detectTarget)
        {
            if (enemyState.TryPatrolState()) return;
            if (enemyState.TryIdleState()) return;
        }
    }
}
