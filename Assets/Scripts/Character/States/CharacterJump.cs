using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterMovementState
{
    public override void Enter(Character _character)
    {
        base.Enter(_character);

        Character.JumpsLeft--;

        Character.Animator.SetTrigger("Jump");

        Character.Collider.isTrigger = true;

        Debug.Log("Jump Enter");
    }

    public override void Exit()
    {
        base.Exit();

        Character.Collider.isTrigger = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        UpdateHorizontalInput();

        targetVelocityY = Character.JumpCurve.Evaluate(TimeInState / Character.JumpTimeMax) * Character.JumpHeight;
        targetVelocityY = Mathf.Lerp(Character.Rigidbody.velocity.y, targetVelocityY, Character.JumpLerpSpeed * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckState()
    {
        base.CheckState();

        if (TimeInState > Character.JumpTimeMin && !Character.Player.InputData.Jump)
            Character.ChangeState(Character.FallState);


        if (TimeInState > Character.JumpTimeMax)
        {
            Character.Player.InputData.Jump = false;
            Character.ChangeState(Character.FallState);
        }

        if (Character.Player.InputData.Dash)
            Character.ChangeState(Character.DashState);
    }
}
