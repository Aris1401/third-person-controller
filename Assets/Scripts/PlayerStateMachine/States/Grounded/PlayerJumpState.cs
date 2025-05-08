using System.Collections;
using UnityEngine;

public class PlayerJumpState : PlayerMovementState
{
    public PlayerJumpState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter(IState lastState = null)
    {
        base.OnEnter(lastState);

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsJumping", true);

        RotateTowardDirection();

        // Applying upward force
        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.ApplyVerticalForce(Mathf.Sqrt(((PlayerStateMachine) this.stateMachine).jumpHeight * -2f * Physics.gravity.y), Vector3.up);
    }

    public void RotateTowardDirection()
    {
        ((PlayerStateMachine)this.stateMachine).transform.rotation = Quaternion.LookRotation(((PlayerStateMachine) this.stateMachine).m_velocityComponent.Direction);
    }

    public override void OnUpdate()
    {
        if (((PlayerStateMachine)this.stateMachine).m_animator.GetCurrentAnimatorStateInfo(0).IsName("BeginJump"))
        {
            if (((PlayerStateMachine)this.stateMachine).m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                this.stateMachine.ChangeState(new PlayerInAirState(this.stateMachine));
            }
        }
        else
        {
            if (((PlayerStateMachine) this.stateMachine).m_velocityComponent.Velocity.y < 0f)
            {
                this.stateMachine.ChangeState(new PlayerInAirState((PlayerStateMachine)this.stateMachine));
            }
        }
    }

    public override void OnFixedUpdate() { }

    public override void OnExit()
    {
        base.OnExit();

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsJumping", false);
    }
}
