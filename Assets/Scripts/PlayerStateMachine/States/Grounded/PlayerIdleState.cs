using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter(IState lastState = null)
    {
        base.OnEnter(lastState);

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsIdle", true);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        if (((PlayerStateMachine)this.stateMachine).m_actions.Player.Move.ReadValue<Vector2>().magnitude > 0)
        {
            if (((PlayerStateMachine)this.stateMachine).m_actions.Player.Sprint.IsPressed())
            {
                this.stateMachine.ChangeState(new PlayerSprintState(this.stateMachine));
            }
            else
            {
                this.stateMachine.ChangeState(new PlayerWalkState(this.stateMachine));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsIdle", false);
    }
}
