using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public CharacterController m_characterController { get; private set; }
    public Animator m_animator { get; private set; }
    public VelocityComponent m_velocityComponent { get; private set; }

    public InputSystem_Actions m_actions { get; private set; }

    [Header("Movement Properties")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;

    public float normalAcceleration = 9.0f;
    public float inAirAcceleration = 1f;
    public float jumpHeight = 4f;

    private void Awake()
    {
        m_actions = new InputSystem_Actions();
        m_actions.Player.Enable();
    }

    private void OnDisable()
    {
        m_actions.Player.Disable();
    }

    protected override void Initialize()
    {
        base.Initialize();
        
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_velocityComponent = GetComponent<VelocityComponent>();

        this.currentState = new PlayerIdleState(this);
    }

    private void OnGUI()
    {
        GUILayout.TextArea(currentState.GetType().Name);
        GUILayout.TextArea("CC: " + m_characterController.isGrounded);
    }
}
