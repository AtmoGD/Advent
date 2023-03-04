using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementState : CharacterState
{
    protected float targetVelocityX = 0f;
    protected float targetVelocityY = 0f;

    public override void Enter(Character _character)
    {
        base.Enter(_character);

        // Check if the previeus state of the character was of type CharacterMovementState
        if (Character.PreviousState is CharacterMovementState)
        {
            targetVelocityX = ((CharacterMovementState)Character.PreviousState).targetVelocityX;
            targetVelocityY = ((CharacterMovementState)Character.PreviousState).targetVelocityY;
        }
        else
        {
            targetVelocityX = 0f;
            targetVelocityY = 0f;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (Character.Rigidbody.velocity.x != 0)
            Character.Animator.SetFloat("Dir", Character.Rigidbody.velocity.x < 0 ? -1 : 1);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        MoveHorizontal();

        MoveVertical();
    }

    public void MoveHorizontal()
    {
        Character.Rigidbody.velocity = new Vector2(targetVelocityX, Character.Rigidbody.velocity.y);
    }

    public void UpdateHorizontalInput()
    {
        targetVelocityX = Character.CurrentInput.Horizontal * Character.Speed;

        if (targetVelocityX == 0)
            targetVelocityX = Mathf.Lerp(Character.Rigidbody.velocity.x, targetVelocityX, Character.DecelerationSpeed * Time.deltaTime);
        else
            targetVelocityX = Mathf.Lerp(Character.Rigidbody.velocity.x, targetVelocityX, Character.AccelerationSpeed * Time.deltaTime);
    }

    public void MoveVertical()
    {
        Character.Rigidbody.velocity = new Vector2(Character.Rigidbody.velocity.x, targetVelocityY);
    }

    public void SetHorizontalVelocity(float _velocity)
    {
        targetVelocityX = _velocity;
    }

    public void SetVerticalVeloctiy(float _velocity)
    {
        targetVelocityY = _velocity;
    }
}
