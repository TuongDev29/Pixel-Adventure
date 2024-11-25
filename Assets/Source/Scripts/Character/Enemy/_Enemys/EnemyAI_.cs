using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_ : MainMonoBehaviour
{
    public float sightRange = 10f; // Tầm nhìn của kẻ thù
    public float attackRange = 2f; // Tầm tấn công của kẻ thù
    [SerializeField] protected List<float> targetOffsets;// Các điểm tuần tra
    [SerializeField] protected int currentPatrolIndex = 0;
    [SerializeField] protected EnemyController enemyCtrl;
    protected Vector2 startPos;

    protected virtual void Start()
    {
        this.startPos = transform.position;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.AutoLoad<EnemyController>(ref this.enemyCtrl, transform.GetComponent<EnemyController>());
    }

    public bool MoveToNextPatrolPoint()
    {
        if (targetOffsets.Count == 0) return false;
        bool isNextPatrolPoint = false;

        float targetX = startPos.x + targetOffsets[currentPatrolIndex];

        // Tính vị trí tiếp theo dựa trên khoảng cách X
        float progress = enemyCtrl.EnemyData.moveSpeed * Time.fixedDeltaTime / Mathf.Abs(targetX - transform.position.x);
        float nextX = Mathf.Lerp(transform.position.x, targetX, progress);

        Vector2 targetPosition = new Vector2(nextX, transform.position.y);

        enemyCtrl.rb.MovePosition(targetPosition);

        // Check distance
        if (Mathf.Abs(targetX - transform.position.x) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % targetOffsets.Count;
            isNextPatrolPoint = true;
        }
        return isNextPatrolPoint;
    }

    public void ChasePlayer()
    {
        Vector2 targetPosition = PlayerManager.Instance.GetCurrentPlayer().position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 3f * Time.deltaTime);
    }

    public void AttackPlayer()
    {
        Debug.Log("Attacking the player!");
        // Logic cho tấn công người chơi, như giảm máu
    }
}

// public class MushroomEnemy : EnemyAI
// {
//     public float specialAbilityCooldown = 5f; // Đặc biệt, có thể có kỹ năng đặc biệt

//     protected void Update()
//     {
//         // base.Update();

//         // Bạn có thể thêm các hành vi đặc biệt cho Mushroom ở đây
//         if (Time.time >= specialAbilityCooldown)
//         {
//             SpecialAbility();
//             specialAbilityCooldown = Time.time + 5f;
//         }
//     }

//     private void SpecialAbility()
//     {
//         // Logic cho khả năng đặc biệt của Mushroom, như gây nọc độc
//         Debug.Log("Mushroom uses special ability!");
//     }
// }


// public class PatrolState : State
// {
//     public override void Enter(EnemyAI enemy)
//     {
//         Debug.Log("Entering Patrol State");
//     }

//     public override void Exit(EnemyAI enemy)
//     {
//         Debug.Log("Exiting Patrol State");
//     }

//     public override void Update(EnemyAI enemy)
//     {
//         // Di chuyển theo vòng tròn hoặc đi qua các điểm tuần tra
//         enemy.MoveToNextPatrolPoint();

//         // Kiểm tra nếu có Player để chuyển sang trạng thái Chase
//         if (Vector2.Distance(enemy.transform.position, enemy.player.position) < enemy.sightRange)
//         {
//             enemy.ChangeState(new ChaseState());
//         }
//     }
// }

// public class ChaseState : IState
// {
//     public  void Enter(EnemyAI enemy)
//     {
//         Debug.Log("Entering Chase State");
//     }

//     public void Exit(EnemyAI enemy)
//     {
//         Debug.Log("Exiting Chase State");
//     }

//     public void 
//     {
//         // Di chuyển về phía người chơi
//         enemy.ChasePlayer();

//         // Kiểm tra nếu đối tượng đuổi kịp người chơi để chuyển sang trạng thái Attack
//         if (Vector2.Distance(enemy.transform.position, enemy.player.position) < enemy.attackRange)
//         {
//             enemy.ChangeState(new AttackState());
//         }
//     }
// }

// public class AttackState : State
// {
//     public override void Enter(EnemyAI enemy)
//     {
//         Debug.Log("Entering Attack State");
//     }

//     public override void Exit(EnemyAI enemy)
//     {
//         Debug.Log("Exiting Attack State");
//     }

//     public override void Update(EnemyAI enemy)
//     {
//         // Tấn công người chơi hoặc gây sát thương
//         enemy.AttackPlayer();

//         // Kiểm tra nếu người chơi chạy xa hoặc chết
//         if (Vector2.Distance(enemy.transform.position, enemy.player.position) > enemy.attackRange)
//         {
//             enemy.ChangeState(new ChaseState());
//         }
//     }
// }
