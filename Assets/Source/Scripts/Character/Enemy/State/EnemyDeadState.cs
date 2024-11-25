using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyController enemy;
    private StateMachine state;

    public EnemyDeadState(EnemyController _enemy, StateMachine _state)
    {
        this.enemy = _enemy;
        this.state = _state;
    }

    public void Enter()
    {
        enemy.anim.SetBool("isDead", true);
    }

    public void Exit()
    {
        enemy.anim.SetBool("isDead", false);
    }

    public void Excute()
    {
    }
}
