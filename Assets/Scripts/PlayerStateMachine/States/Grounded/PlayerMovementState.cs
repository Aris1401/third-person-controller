using UnityEngine;

public class PlayerMovementState : PlayerGroundedState
{
    protected Vector2 input;
    protected Vector3 hVelocity = new Vector3();

    public PlayerMovementState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter(IState lastState = null)
    {
        base.OnEnter(lastState);

        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.Speed = ((PlayerStateMachine)this.stateMachine).walkSpeed;
        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.Acceleration = ((PlayerStateMachine)this.stateMachine).normalAcceleration;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.RotateDirectionToward(input, camForward, camRight);
        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.AccelerateTowardDirection();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        input = ((PlayerStateMachine)this.stateMachine).m_actions.Player.Move.ReadValue<Vector2>();

        if (input.magnitude == 0f && ((PlayerStateMachine)this.stateMachine).m_velocityComponent.GetHorizontalVelocityMagnitude() <= 0.2f)
        {
            this.stateMachine.ChangeState(new PlayerIdleState((PlayerStateMachine)this.stateMachine));
        }
    }
}
