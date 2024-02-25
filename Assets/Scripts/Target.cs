using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float impactForce = 500;
    public void TakeDamage(RaycastHit info, Transform camPos)
    {
        if (info.collider.CompareTag("Enemy") == true)
        {
            Death(info);
        }
        else
        {
            Vector3 direction = info.transform.position - camPos.transform.position;
            info.rigidbody.AddForce(direction * impactForce);
        }
        
    }

    public void Death(RaycastHit character)
    {
        character.transform.Rotate(-Vector3.right,90);
        //character.transform.Translate(Vector3.up);
    }


}