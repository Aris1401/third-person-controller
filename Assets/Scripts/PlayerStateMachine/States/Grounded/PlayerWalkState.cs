using UnityEditor;
using UnityEngine;

public class PlayerWalkState : PlayerMovementState
{
    Vector2 inputSmoothing = new Vector2();

    Vector3 m_lookAtPosition = new Vector3();

    public PlayerWalkState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter(IState lastState = null)
    {
        base.OnEnter(lastState);
        ((PlayerStateMachine) this.stateMachine).m_animator.SetBool("IsWalking", true);

        ((PlayerStateMachine)this.stateMachine).m_velocityComponent.Speed = ((PlayerStateMachine)this.stateMachine).walkSpeed;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.HandlePlayerRotation();
        this.HandleAnimations();

        if (((PlayerStateMachine)this.stateMachine).m_actions.Player.Sprint.WasPressedThisFrame())
        {
            this.stateMachine.ChangeState(new PlayerSprintState(this.stateMachine));
        }
    }

    public void HandleAnimations()
    {
        inputSmoothing = Vector2.Lerp(inputSmoothing, input, 9f * Time.deltaTime);

        ((PlayerStateMachine)this.stateMachine).m_animator.SetFloat("MovementX", this.inputSmoothing.x);
        ((PlayerStateMachine)this.stateMachine).m_animator.SetFloat("MovementY", Mathf.Clamp(this.inputSmoothing.y, -1, 0));
    }

    public override void OnExit()
    {
        base.OnExit();

        ((PlayerStateMachine)this.stateMachine).m_animator.SetBool("IsWalking", false);
    }

    void HandlePlayerRotation()
    {
        if (((PlayerStateMachine) this.stateMachine).m_velocityComponent.Direction.magnitude > 0f)
        {
            Vector3 camForward = Camera.main.transform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            ((PlayerStateMachine)this.stateMachine).transform.rotation = Quaternion.Slerp(((PlayerStateMachine)this.stateMachine).transform.rotation, Quaternion.LookRotation(camForward), 9f * Time.deltaTime);
        }
    }
}
