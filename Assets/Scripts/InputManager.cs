using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static PlayerControls controls;

    public static Vector2 movementInput;
    public static Vector2 TurnInput;

    public static event System.Action OnJumpInput;
    public static event System.Action OnShootInput;
    public static event System.Action OnInteractInput;
    public static event System.Action OnScrollInput;
    public static event System.Action OnInventoryInput;

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

        controls.Player.Jump.performed += ctx => OnJumpInput?.Invoke();
        controls.Player.Shoot.performed += ctx => OnShootInput?.Invoke();
        controls.Player.Scroll.performed += ctx => OnScrollInput?.Invoke();
        controls.Player.Interact.performed += ctx => OnInteractInput?.Invoke();
        controls.Player.ToggleInventory.performed += ctx => OnInventoryInput?.Invoke();

        controls.Player.Enable();
    }
    void OnDisable()
    {
        controls.Player.Move.performed -= Move;
        controls.Player.Move.canceled -= Move;

        controls.Player.Turn.performed -= Turn;
        controls.Player.Turn.canceled -= Turn;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }
    private void Turn(InputAction.CallbackContext ctx)
    {
        TurnInput = ctx.ReadValue<Vector2>();
    }
}