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

        if (enemyCtrl.Context.patrolsDistance.Count == 0) return;

        float targetX = enemyCtrl.Context.startPos.x + enemyCtrl.Context.patrolsDistance[enemyCtrl.Context.currentPatrolIndex];

        // Tính vị trí tiếp theo dựa trên khoảng cách X
        float progress = enemyCtrl.EnemyData.moveSpeed * Time.fixedDeltaTime / Mathf.Abs(targetX - transform.position.x);
        float nextX = Mathf.Lerp(transform.position.x, targetX, progress);

        Vector2 targetPosition = new Vector2(nextX, transform.position.y);

        enemyCtrl.rb.MovePosition(targetPosition);

        // Check distance
        if (Mathf.Abs(targetX - transform.position.x) < 0.1f)
        {
            enemyCtrl.Directional.ToggleDirection();

            enemyCtrl.Context.currentPatrolIndex = (enemyCtrl.Context.currentPatrolIndex + 1) % enemyCtrl.Context.patrolsDistance.Count;
            enemyState.ChangeState(EnemyStateMachine.EEnemyState.Idle, 0.4f);
        }

        if (enemyState.TryChaseState()) return;
    }
}
