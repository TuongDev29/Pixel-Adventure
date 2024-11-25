using System;
using UnityEngine;

[Serializable]
public class PlayerDirectional : Directional
{
    private PlayerController playerCtrl;

    public PlayerDirectional(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
    }
}
