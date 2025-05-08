using UnityEngine;

public interface IState
{
    public void OnEnter(IState lastState = null);
    public void OnExit();

    public void OnUpdate();
    public void OnFixedUpdate();
    public void OnDrawGizmosSelected();
}
