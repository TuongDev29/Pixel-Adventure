using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContext : MainMonoBehaviour
{
    public bool canChasePlayer;
    public List<float> patrolsDistance;
    public Vector2 startPos;
    public int currentPatrolIndex = 0;
    public float sightRange = 10f;
    public float attackRange = 2f;

    private void OnEnable()
    {
        this.startPos = transform.position;
    }
}