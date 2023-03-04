using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : CharacterMovementState
{
    Vector2 dashDirection;

    public override void Enter(Character _character)
    {
        base.Enter(_character);

        Character.Animator.SetTrigger("Dash");

        dashDirection = Character.CurrentInput.Direction;

        Character.Player.InputData.Dash = false;

        Character.Collider.isTrigger = true;

        Debug.Log("Dash Enter");
    }

    public override void Exit()
    {
        base.Exit();

        Character.Collider.isTrigger = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Vector2 velocity = dashDirection * Character.DashSpeed;
        targetVelocityX = velocity.x;
        targetVelocityY = velocity.y;

        // targetVelocityX = Character.DashCurve.Evaluate(TimeInState / Character.DashTime) * Character.DashSpeed;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckState()
    {
        base.CheckState();

        if (TimeInState > Character.DashTime)
            Character.ChangeState(Character.FallState);
    }
}