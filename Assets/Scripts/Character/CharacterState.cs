using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
    public Character Character { get; protected set; } = null;
    public float TimeInState { get; protected set; } = 0;

    public virtual void Enter(Character _character)
    {
        Character = _character;
        TimeInState = 0;
    }

    public virtual void FrameUpdate()
    {
        TimeInState += Time.deltaTime;

        CheckState();
    }

    public virtual void PhysicsUpdate() { }

    public virtual void CheckState() { }

    public virtual void Exit() { }
}
