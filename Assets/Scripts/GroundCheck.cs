using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f;

    void Update()
    {
        groundCheckDistance = (this.GetComponent<CapsuleCollider>().height / 2) + bufferCheckDistance;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}