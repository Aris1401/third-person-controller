using UnityEngine;

public class PlayerGroundedState : BaseState
{
    public PlayerGroundedState(StateMachine stateMachine) : base(stateMachine) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!((PlayerStateMachine) this.stateMachine).m_velocityComponent.IsGrounded)
        {
            ((PlayerStateMachine) this.stateMachine).ChangeState(new PlayerInAirState(this.stateMachine));
        }

        this.CheckJump();
    }

    public void CheckJump()
    {
        if (((PlayerStateMachine)this.stateMachine).m_actions.Player.Jump.WasPressedThisFrame() && ((PlayerStateMachine) this.stateMachine).m_characterController.isGrounded)
        {
            this.stateMachine.ChangeState(new PlayerJumpState(this.stateMachine));
        }
    }
}
