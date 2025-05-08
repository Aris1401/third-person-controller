using UnityEngine;

public abstract class BaseState : IState
{
    [SerializeField] protected StateMachine stateMachine;

    public BaseState(StateMachine stateMachine) { 
        this.stateMachine = stateMachine;
    }

    public virtual void OnDrawGizmosSelected() { }

    public virtual void OnEnter(IState lastState = null) { }

    public virtual void OnExit() { }

    public virtual void OnFixedUpdate() { }

    public virtual void OnUpdate() { }
}
