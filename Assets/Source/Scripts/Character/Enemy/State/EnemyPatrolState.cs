using UnityEngine;

public class EnemyPatrolState : IState
{
    private EnemyController enemyCtrl;
    private EnemyStateMachine enemyState;
    private Transform transform;

    public EnemyPatrolState(EnemyController _enemy, EnemyStateMachine _state)
    {
        this.enemyCtrl = _enemy;
        this.enemyState = _state;

        this.transform = enemyCtrl.transform;
    }

    public void Enter()
    {
        enemyCtrl.anim.SetBool("isRunning", true);
    }

    public void Exit()
    {
        enemyCtrl.anim.SetBool("isRunning", false);
    }

    public void Excute()
    {
        if (this.enemyState.TryDeadState()) return;

        bool isNextPatrolPoint = enemyCtrl.EnemyAI.MoveNextToPatrolPoint();
        if (isNextPatrolPoint)
        {
            if (enemyState.TryIdleState()) return;
        }

        if (this.enemyState.TryChaseState()) return;
    }
}
