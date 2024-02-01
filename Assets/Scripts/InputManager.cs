using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    PlayerControls controls;

    public static Vector2 movementInput;
    public static Vector2 TurnInput;
    public static bool OnJumpInput;

    public static event Action onJumpStart;
    public static event Action onJumpCancelled;


    private void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Player.Move.performed += Move;
        controls.Player.Move.canceled += Move;

        controls.Player.Turn.performed += Turn;
        controls.Player.Turn.canceled += Turn;

        controls.Player.Jump.performed += ctx => onJumpStart.Invoke();
        controls.Player.Jump.canceled += ctx => onJumpCancelled.Invoke();

        controls.Player.Enable();
    }
    void OnDisable()
    {
        controls.Player.Move.performed -= Move;
        controls.Player.Move.canceled -= Move;

        controls.Player.Turn.performed -= Turn;
        controls.Player.Turn.canceled -= Turn;

        controls.Player.Jump.performed -= Jump;
        controls.Player.Jump.canceled -= Jump;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }
    private void Turn(InputAction.CallbackContext ctx)
    {
        TurnInput = ctx.ReadValue<Vector2>();
    }
    private void Jump(InputAction.CallbackContext ctx)
    {
        OnJumpInput = ctx.ReadValueAsButton();
    }
}
