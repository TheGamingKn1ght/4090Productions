using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls controls;

    public static Vector2 movementInput;
    public static Vector2 TurnInput;
    

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
