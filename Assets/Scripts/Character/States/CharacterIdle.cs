using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : CharacterMovementState
{
    public override void Enter(Character _character)
    {
        base.Enter(_character);

        targetVelocityX = 0f;
        targetVelocityY = 0f;

        Character.Animator.SetTrigger("Idle");

        Debug.Log("Idle Enter");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        targetVelocityY = Character.BaseGravity * Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckState()
    {
        base.CheckState();

        if (!Character.IsGrounded)
            Character.ChangeState(Character.FallState);

        Character.JumpsLeft = Character.MaxJumps;
        Character.LastGroundedTime = Time.time;

        if (Character.CurrentInput.Jump && Character.CanJump)
            Character.ChangeState(Character.JumpState);

        if (Character.CurrentInput.Horizontal != 0)
            Character.ChangeState(Character.RunState);

        if (Character.Player.InputData.Dash)
            Character.ChangeState(Character.DashState);
    }
}
