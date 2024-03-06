using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float damage = 25;
    public float impactForce = 500;

    public void TakeDamage(RaycastHit info, Transform camPos)
    {
        if (info.rigidbody.CompareTag("Enemy") == true)
        {
            Death(info);
        }
        else
        {
            //AudioManager.Singleton.PlaySoundEffect("Riccochet");
            Vector3 direction = info.transform.position - camPos.transform.position;
            info.rigidbody.AddForce(direction * impactForce);
        }
        
    }

    public void DealDamage(Transform camPos)
    {
        HealthBar.health += damage;
        RaycastHit hit;
        if(Physics.Raycast(camPos.transform.position, camPos.transform.forward, out hit, 2))
        {
            HealthBar.health += damage;
        }
    }

    public void Death(RaycastHit character)
    {
        agent.GetComponent<NavMeshAgent>().enabled = false;
        character.transform.Rotate(-Vector3.right,90);
        //character.transform.Translate(Vector3.up);
    }


}