using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private float yClamp = 60;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 lookInput = InputManager.TurnInput;
        float mouseX = lookInput.x * Time.deltaTime * sensitivity;
        float mouseY = lookInput.y * Time.deltaTime * sensitivity;

        float xRotation = 0;
        xRotation = Mathf.Clamp(xRotation, -yClamp, yClamp);

        Vector3 rot = transform.localEulerAngles;
        
        float desiredX = rot.y + mouseX;
        xRotation = xRotation - mouseY;

        transform.Rotate(Vector3.up * (lookInput.x * sensitivity));
        xRotation -= lookInput.x * sensitivity;
        transform.localEulerAngles = new Vector3(xRotation, desiredX, 0);
    }
}
