using UnityEngine;

public class PlayerLandState : BaseState
{
    public PlayerLandState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter(IState lastState = null)
    {
        base.OnEnter(lastState);

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsLanding", true);

        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.ResetVelocity();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (((PlayerStateMachine) this.stateMachine).m_animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            if (((PlayerStateMachine) this.stateMachine).m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                this.stateMachine.ChangeState(new PlayerIdleState((PlayerStateMachine) this.stateMachine));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsLanding", false);
    }
}
