using System;
using UnityEngine;

[Serializable]
public class EnemyDirectional : Directional
{
    private PlayerController playerCtrl;

    public EnemyDirectional(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
    }
}
