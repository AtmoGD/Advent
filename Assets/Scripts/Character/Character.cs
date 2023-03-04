using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Animator Animator = null;
    [field: SerializeField] public Rigidbody2D Rigidbody { get; set; }
    [field: SerializeField] public Collider2D Collider { get; set; }
    [field: SerializeField] public Transform GroundCheck { get; set; }
    [field: SerializeField] public LayerMask GroundMask { get; set; }

    [Header("Settings")]
    [SerializeField] public float GroundCheckRadius = 0.2f;
    [field: SerializeField] public Vector2 GroundCheckSize { get; set; }
    [field: SerializeField] public float ZeroThreshold { get; set; } = 0.03f;

    [Header("Running")]
    [SerializeField] public float AccelerationSpeed = 0f;
    [field: SerializeField] public float DecelerationSpeed { get; set; }
    [field: SerializeField] public float Speed { get; set; }

    [Header("Jumping")]
    [SerializeField] public float JumpHeight = 0f;
    [field: SerializeField] public int MaxJumps { get; set; }
    [field: SerializeField] public AnimationCurve JumpCurve { get; set; }
    [field: SerializeField] public float JumpTimeMin { get; set; }
    [field: SerializeField] public float JumpTimeMax { get; set; }
    [field: SerializeField] public float JumpLerpSpeed { get; set; }
    [field: SerializeField] public float CoyoteTime { get; set; }

    [Header("Falling")]
    [SerializeField] public float FallSpeed = 0f;
    [field: SerializeField] public float BaseGravity { get; set; }
    [field: SerializeField] public AnimationCurve FallCurve { get; set; }
    [field: SerializeField] public float FallTime { get; set; }
    [field: SerializeField] public float FallLerpSpeed { get; set; }

    [Header("Dash")]
    [SerializeField] public float DashSpeed = 3f;
    [field: SerializeField] public float DashTime { get; set; }
    // [field: SerializeField] public AnimationCurve DashCurve { get; set; }



    public CharacterState CurrentState { get; protected set; }
    public CharacterState PreviousState { get; protected set; }
    public CharacterIdle IdleState { get; protected set; }
    public CharacterRun RunState { get; protected set; }
    public CharacterJump JumpState { get; protected set; }
    public CharacterFall FallState { get; protected set; }
    public CharacterDash DashState { get; protected set; }

    [Header("Other")]
    [SerializeField] public InputData CurrentInput = new InputData();
    [field: SerializeField] public Player Player { get; set; }


    public bool IsGrounded { get { return Physics2D.OverlapBox(GroundCheck.position, GroundCheckSize, 0, GroundMask); } }
    public bool WasGrounded { get { return (Time.time - LastGroundedTime) < CoyoteTime; } }
    public bool CanJump { get { return JumpsLeft > 0; } }
    public float LastGroundedTime { get; set; }
    public int JumpsLeft { get; set; }

    private void Awake()
    {
        IdleState = new CharacterIdle();
        RunState = new CharacterRun();
        JumpState = new CharacterJump();
        FallState = new CharacterFall();
        DashState = new CharacterDash();

        CurrentInput = new InputData();

        LastGroundedTime = 0f;
        JumpsLeft = 0;

        ChangeState(IdleState);
    }

    public void FrameUpdate()
    {
        CurrentState?.FrameUpdate();
    }

    public void PhysicsUpdate()
    {
        CurrentState?.PhysicsUpdate();
    }

    public void Enter(Player _player)
    {
        Player = _player;

        ChangeState(IdleState);
    }

    public void Exit()
    {
        ChangeState(null);
    }

    public void ChangeState(CharacterState _newState)
    {
        CurrentState?.Exit();
        PreviousState = CurrentState;
        CurrentState = _newState;
        CurrentState?.Enter(this);
    }


#if UNITY_EDITOR
    [field: SerializeField] public bool Debug { get; set; } = true;
    private void OnDrawGizmos()
    {
        if (Debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GroundCheck.position, GroundCheckSize);
        }
    }
#endif
}
