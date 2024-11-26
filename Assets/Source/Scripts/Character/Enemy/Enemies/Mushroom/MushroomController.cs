using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : EnemyController
{
    protected override EnemyStateMachine InitEnemyStateMachine()
    {
        return new MushroomStateMachine(this);
    }
}