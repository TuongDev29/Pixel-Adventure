using System.Collections;
using UnityEngine;

public class X : CollisionDirectionChecker<X>, ICheckCollisionDown
{


    public X(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
    }

    public bool CheckCollisionDown()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnChecking()
    {
        throw new System.NotImplementedException();
    }
}