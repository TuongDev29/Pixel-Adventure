using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MainMonoBehaviour
{
    [SerializeField] protected List<Vector2> patrolPoints;
    [SerializeField] protected int currentPatrolPointIndex;
    [SerializeField] protected EnemyController enemyCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.AutoLoad<EnemyController>(ref this.enemyCtrl, transform.GetComponent<EnemyController>());
    }

    public void SetPatrolPoints(List<Vector2> _patrolPoints)
    {
        this.patrolPoints = _patrolPoints;
    }

    public bool MoveNextToPatrolPoint()
    {
        if (this.patrolPoints.Count == 0) return false;

        bool isNextPatrolPoint = false;
        Vector2 position = enemyCtrl.rb.position;
        Vector2 currentPatrolPoint = patrolPoints[currentPatrolPointIndex];

        //Move to patrol point
        Vector2 targetPosition = new Vector2(currentPatrolPoint.x, position.y);
        Vector2 nextPosition = Vector2.MoveTowards(position, targetPosition, enemyCtrl.EnemyData.moveSpeed * Time.fixedDeltaTime);
        enemyCtrl.rb.MovePosition(nextPosition);

        // Check Distance and Change Direction
        if (Vector2.Distance(position, targetPosition) < 0.1f)
        {
            this.currentPatrolPointIndex = (this.currentPatrolPointIndex + 1) % this.patrolPoints.Count;
            isNextPatrolPoint = true;
        }

        return isNextPatrolPoint;
    }

    public bool ChasePlayer()
    {
        bool detectTarget = false;

        Vector2 position = enemyCtrl.rb.position;
        Vector2 playerPosition = PlayerManager.Instance.GetPosition();
        Vector2 targetPosition = new Vector2(playerPosition.x, position.y);

        if (Vector2.Distance(position, targetPosition)
            < enemyCtrl.EnemyData.detectionRange)
        {
            Vector2 nextPosition = Vector2.MoveTowards(position, targetPosition, enemyCtrl.EnemyData.moveSpeed * Time.fixedDeltaTime);
            enemyCtrl.rb.MovePosition(nextPosition);

            //
            enemyCtrl.Directional.ChangeDirection(enemyCtrl.rb.velocity.normalized);
            detectTarget = true;
        }

        return detectTarget;
    }
}