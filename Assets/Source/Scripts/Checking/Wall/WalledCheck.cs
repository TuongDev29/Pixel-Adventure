using System.Collections.Generic;
using UnityEngine;

public abstract class WalledCheck<T> : CollisionDirectionChecker<T> where T : class, ICheckCollisionLeft, ICheckCollisionRight
{
    private bool previousWalledState;
    public bool IsWalled
    {
        get
        {
            return IsCollisionLeft || IsCollisionRight;
        }
    }
    private List<IObserveWalled> observeWalleds;

    protected WalledCheck(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
        this.observeWalleds = new List<IObserveWalled>();
    }

    public void RegisterObserveWalled(IObserveWalled observeWalled)
    {
        if (this.observeWalleds.Contains(observeWalled)) return;
        this.observeWalleds.Add(observeWalled);
    }

    protected override void OnChecking()
    {
        if (this.IsWalled)
        {
            if (!this.previousWalledState)
                this.NotifyWalledEnter();

            this.NotifyWalledStay();
        }
        else
        {
            if (this.previousWalledState)
                this.NotifyWalledExit();
        }
        this.previousWalledState = this.IsWalled;
    }

    private void NotifyWalledEnter()
    {
        foreach (IObserveWalled observeWalled in observeWalleds)
        {
            observeWalled.OnWalledEnter();
        }
    }

    private void NotifyWalledExit()
    {
        foreach (IObserveWalled observeWalled in observeWalleds)
        {
            observeWalled.OnWalledExit();
        }
    }

    private void NotifyWalledStay()
    {
        foreach (IObserveWalled observeWalled in observeWalleds)
        {
            observeWalled.OnWalledStay();
        }
    }
}
