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
    }

    public void Exit()
    {
    }

    public void Excute()
    {
        if (enemyState.TryDeadState()) return;

        Vector2 targetPosition = PlayerManager.Instance.GetPosition();
        enemyCtrl.transform.position = Vector2.MoveTowards(enemyCtrl.transform.position, targetPosition, 3f * Time.deltaTime);
    }
}
