using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    private bool canTransition;
    public bool CanTransition => canTransition;
    public IState stateAction;
    protected Dictionary<Enum, IState> state;
    protected MonoBehaviour monoBehaviour;
    private IEnumerator DelayTransitionAction;

    public StateMachine(MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        this.canTransition = true;
        this.state = RegisterState();

        monoBehaviour.StartCoroutine(DelayInitializeState());
    }

    protected virtual Dictionary<Enum, IState> RegisterState()
    {
        return new Dictionary<Enum, IState>();
    }

    protected void AddState(Enum stateKey, IState newState)
    {
        if (!state.ContainsKey(stateKey))
            this.state.Add(stateKey, newState);
        else
            Debug.LogWarning("StateKey: " + stateKey.ToString() + " to exits");
    }

    protected IState GetState(Enum stateKey)
    {

        if (this.state.ContainsKey(stateKey))
        {
            return state[stateKey];
        }
        else
            Debug.LogError("Not FOUND stateKey: " + stateKey.ToString());
        return null;
    }

    public virtual void UpdateState()
    {
        this.stateAction?.Excute();
    }

    int count = 0;
    public void ChangeState(Enum stateKey)
    {
        if (!this.canTransition) return;
        if (this.CompareState(stateKey)) return;

        // Debug.Log(stateKey.ToString() + (count++));
        stateAction?.Exit();
        state[stateKey].Enter();
        stateAction = state[stateKey];
    }

    public void ChangeState(Enum stateKey, float delayTransitionTimer)
    {
        if (!this.canTransition) return;

        this.ChangeState(stateKey);
        this.DelayTransition(delayTransitionTimer);
    }

    public void DelayTransition(float timer)
    {

        if (this.DelayTransitionAction != null)
            this.monoBehaviour.StopCoroutine(this.DelayTransitionAction);

        this.DelayTransitionAction = StartDelayTransition(timer);
        this.monoBehaviour.StartCoroutine(DelayTransitionAction);
    }

    public bool CompareState(Enum stateKey)
    {
        if (this.state.ContainsKey(stateKey) && this.stateAction == state[stateKey])
            return true;
        return false;
    }

    public void EnableTransition()
    {
        if (this.DelayTransitionAction != null)
            this.monoBehaviour.StopCoroutine(this.DelayTransitionAction);
        this.canTransition = true;
    }

    public void DisableTransition()
    {
        if (this.DelayTransitionAction != null)
            this.monoBehaviour.StopCoroutine(this.DelayTransitionAction);
        this.canTransition = false;
    }

    private IEnumerator StartDelayTransition(float timer)
    {
        this.canTransition = false;

        yield return new WaitForSeconds(timer);
        this.canTransition = true;
    }

    private IEnumerator DelayInitializeState()
    {
        yield return null;
        this.InitializeState();
    }
    protected abstract void InitializeState();
}
