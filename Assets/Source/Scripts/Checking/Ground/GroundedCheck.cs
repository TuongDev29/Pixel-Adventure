using System.Collections.Generic;
using UnityEngine;

public abstract class GroundedCheck<T> : CollisionDirectionChecker<T> where T : class, ICheckCollisionDown
{
    private bool previousGroundedState;
    public bool IsGrounded
    {
        get
        {
            return IsCollisionDown;
        }
    }
    private List<IObserveGrounded> observeGroundeds;

    protected GroundedCheck(MonoBehaviour monoBehaviour) : base(monoBehaviour)
    {
        observeGroundeds = new List<IObserveGrounded>();
    }

    public void RegisterObserveGrounded(IObserveGrounded observeGrounded)
    {
        if (observeGroundeds.Contains(observeGrounded)) return;
        observeGroundeds.Add(observeGrounded);
    }

    protected override void OnChecking()
    {
        if (this.IsGrounded)
        {
            if (!this.previousGroundedState)
                this.NotifyGroundedEnter();

            this.NotifyGroundedStay();
        }
        else
        {
            if (this.previousGroundedState)
                this.NotifyGroundedExit();
        }
        this.previousGroundedState = this.IsCollisionDown;
    }

    private void NotifyGroundedEnter()
    {
        foreach (IObserveGrounded observeGrounded in observeGroundeds)
        {
            observeGrounded.OnGroundedEnter();
        }
    }

    private void NotifyGroundedExit()
    {
        foreach (IObserveGrounded observeGrounded in observeGroundeds)
        {
            observeGrounded.OnGroundedExit();
        }
    }

    private void NotifyGroundedStay()
    {
        foreach (IObserveGrounded observeGrounded in observeGroundeds)
        {
            observeGrounded.OnGroundedStay();
        }
    }
}
