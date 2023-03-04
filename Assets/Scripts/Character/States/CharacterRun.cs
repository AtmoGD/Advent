using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRun : CharacterMovementState
{
    public override void Enter(Character _character)
    {
        base.Enter(_character);

        Character.Animator.SetTrigger("Run");

        targetVelocityX = Character.Rigidbody.velocity.magnitude * Character.CurrentInput.Horizontal;

        Debug.Log("Run Enter");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        UpdateHorizontalInput();
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

        if (Character.CurrentInput.Horizontal == 0 && Mathf.Abs(Character.Rigidbody.velocity.x) < Character.ZeroThreshold)
            Character.ChangeState(Character.IdleState);

        if (Character.Player.InputData.Dash)
            Character.ChangeState(Character.DashState);
    }
}
