using UnityEngine;

public class MoveToPatrolPoint : IEnemyAIBehavior
{
    protected EnemyController enemyCtrl;
    protected Transform transform;
    protected int currentPatrolIndex;
    protected Vector2 startPos;

    public MoveToPatrolPoint(EnemyController enemyController)
    {
        this.enemyCtrl = enemyController;
        this.transform = enemyController.transform;
    }

    public void PerformBehavior()
    {
        // if (targetOffsets.Count == 0) return;

        float targetX = startPos.x;//+ targetOffsets[currentPatrolIndex];

        // Tính vị trí tiếp theo dựa trên khoảng cách X
        float progress = enemyCtrl.EnemyData.moveSpeed * Time.fixedDeltaTime / Mathf.Abs(targetX - transform.position.x);
        float nextX = Mathf.Lerp(transform.position.x, targetX, progress);

        Vector2 targetPosition = new Vector2(nextX, transform.position.y);

        enemyCtrl.rb.MovePosition(targetPosition);

        // Check distance
        if (Mathf.Abs(targetX - transform.position.x) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1);//% targetOffsets.Count;
        }
        return;
    }
}
