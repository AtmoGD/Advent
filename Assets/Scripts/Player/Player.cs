using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class InputData
{
    [field: SerializeField] public float Horizontal { get; set; } = 0f;
    [field: SerializeField] public float Vertical { get; set; } = 0f;
    public Vector2 Direction => new Vector2(Horizontal, Vertical).normalized;
    [field: SerializeField] public bool Jump { get; set; } = false;
    [field: SerializeField] public bool Dash { get; set; } = false;
}

public class Player : MonoBehaviour
{
    [field: SerializeField] public Character BaseForm { get; protected set; } = null;
    [field: SerializeField] public Character CurrentForm { get; protected set; } = null;
    [field: SerializeField] public InputData InputData { get; set; } = new InputData();

    private void Awake()
    {
        ChangeForm(BaseForm);

        InputData = new InputData();
    }

    private void Update()
    {
        CurrentForm.CurrentInput = InputData;
        CurrentForm?.FrameUpdate();
    }

    private void FixedUpdate()
    {
        CurrentForm?.PhysicsUpdate();
    }

    public void ChangeForm(Character _newForm)
    {
        CurrentForm?.Exit();
        CurrentForm = _newForm;
        CurrentForm?.Enter(this);
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        Vector2 input = _context.ReadValue<Vector2>();
        InputData.Horizontal = input.x;
        InputData.Vertical = input.y;
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.started)
            InputData.Jump = true;
        else if (_context.canceled)
            InputData.Jump = false;
    }

    public void OnDash(InputAction.CallbackContext _context)
    {
        if (_context.started)
            InputData.Dash = true;
        else if (_context.canceled)
            InputData.Dash = false;
    }
}
