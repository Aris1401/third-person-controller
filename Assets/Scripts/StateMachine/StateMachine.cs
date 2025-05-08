using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected IState currentState;

    protected void Start()
    {
        Initialize();

        if (currentState != null) currentState.OnEnter();
    }

    protected void Update()
    {
        if (currentState != null) currentState.OnUpdate();        
    }

    protected private void FixedUpdate()
    {
        if (currentState != null) currentState.OnFixedUpdate();
    }

    protected private void OnDrawGizmosSelected()
    {
        if (currentState != null) currentState.OnDrawGizmosSelected();
    }

    protected virtual void Initialize() { }
    public void ChangeState(IState state)
    {
        if (currentState.Equals(state)) return;

        var lastState = currentState;

        if (currentState != null)
        {
            currentState.OnExit();
            currentState = null;
        }

        currentState = state;
        currentState.OnEnter(lastState);
    }
}
