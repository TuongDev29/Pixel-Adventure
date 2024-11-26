using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : EnemyController
{
    protected override EnemyStateMachine InitEnemyStateMachine()
    {
        return new ChickenStateMachine(this);
    }
}