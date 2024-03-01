using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public float impactForce = 500;
    public void TakeDamage(RaycastHit info, Transform camPos)
    {
        if (info.rigidbody.CompareTag("Enemy") == true)
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
        agent.GetComponent<NavMeshAgent>().enabled = false;
        character.transform.Rotate(-Vector3.right,90);
        //character.transform.Translate(Vector3.up);
    }


}