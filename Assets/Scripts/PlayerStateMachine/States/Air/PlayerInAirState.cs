using UnityEditor;
using UnityEngine;

public class PlayerInAirState : BaseState
{
    const float LAND_THRESHOLD = 2.0f;

    private Vector3 m_startPosition = Vector3.zero;

    IState lastState = null;

    public PlayerInAirState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter(IState lastState = null)
    {
        base.OnEnter(lastState);

        this.lastState = lastState;

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsFalling", true);

        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.Acceleration = ((PlayerStateMachine)this.stateMachine).inAirAcceleration;

        m_startPosition = ((PlayerStateMachine)this.stateMachine).transform.position;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (((PlayerStateMachine) this.stateMachine).m_velocityComponent.IsGrounded)
        {
            float calculatedHeight = Mathf.Abs(m_startPosition.y - ((PlayerStateMachine)this.stateMachine).transform.position.y);
            
            if (calculatedHeight > LAND_THRESHOLD)
            {
                this.stateMachine.ChangeState(new PlayerLandState(this.stateMachine));
            } else
            {
                this.stateMachine.ChangeState(lastState.GetType().Equals(this.GetType()) ? new PlayerIdleState((PlayerStateMachine)this.stateMachine) : lastState);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsFalling", false);
    }
}
