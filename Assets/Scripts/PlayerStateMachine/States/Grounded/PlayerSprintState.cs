using UnityEngine;

public class PlayerSprintState : PlayerMovementState
{
    public PlayerSprintState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter(IState lastState = null)
    {
        base.OnEnter(lastState);
        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsSprinting", true);

        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.Speed = ((PlayerStateMachine)this.stateMachine).sprintSpeed;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.HandlePlayerRotation();

        if (((PlayerStateMachine) this.stateMachine).m_actions.Player.Sprint.WasReleasedThisFrame())
        {
            this.stateMachine.ChangeState(new PlayerWalkState(this.stateMachine));
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsSprinting", false);
    }

    void HandlePlayerRotation()
    {
        if (((PlayerStateMachine) this.stateMachine).m_velocityComponent.Direction.magnitude > 0f)
        {
            ((PlayerStateMachine)this.stateMachine).transform.rotation = Quaternion.Slerp(((PlayerStateMachine)this.stateMachine).transform.rotation, ((PlayerStateMachine)this.stateMachine).m_velocityComponent.GetDirectionRotation(), 9f * Time.deltaTime);
        }
    }
}
