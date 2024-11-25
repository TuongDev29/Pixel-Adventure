using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MainMonoBehaviour
{
    protected Dictionary<EEnemyBehavior, IEnemyAIBehavior> enemyBehaviors;

    public enum EEnemyBehavior
    {
        MoveToPatrolPoint
    }

    protected override void Awake()
    {
        base.Awake();
        enemyBehaviors = new Dictionary<EEnemyBehavior, IEnemyAIBehavior>();
    }

    protected override void EndLoad()
    {
        base.EndLoad();
        this.InitializeBehavior();
    }

    protected void AddEnemyBehavior(EEnemyBehavior enemyBehaviorKey, IEnemyAIBehavior enemyAIBehavior)
    {
        if (!this.enemyBehaviors.ContainsKey(enemyBehaviorKey))
        {
            this.enemyBehaviors.Add(enemyBehaviorKey, enemyAIBehavior);
        }
    }

    public void PerformEnemyBehavior(EEnemyBehavior enemyBehaviorKey)
    {
        if (this.enemyBehaviors.ContainsKey(enemyBehaviorKey))
        {
            this.enemyBehaviors[enemyBehaviorKey].PerformBehavior();
        }
    }

    protected abstract void InitializeBehavior();
}