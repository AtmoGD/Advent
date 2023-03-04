using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFall : CharacterMovementState
{
    public override void Enter(Character _character)
    {
        base.Enter(_character);

        Character.Animator.SetTrigger("Fall");

        Debug.Log("Fall Enter");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        UpdateHorizontalInput();

        targetVelocityY = Character.FallCurve.Evaluate(TimeInState / Character.FallTime) * Character.FallSpeed;
        targetVelocityY = Mathf.Lerp(Character.Rigidbody.velocity.y, targetVelocityY, Character.FallLerpSpeed * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckState()
    {
        base.CheckState();

        if (Character.CurrentInput.Jump && Character.CanJump)
            Character.ChangeState(Character.JumpState);

        if (Character.IsGrounded)
            Character.ChangeState(Character.IdleState);

        if (Character.Player.InputData.Dash)
            Character.ChangeState(Character.DashState);
    }
}
