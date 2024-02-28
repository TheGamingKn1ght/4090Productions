using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTracking : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    private bool isChasing;

    private void Update()
    {
        if(agent.GetComponent<NavMeshAgent>().isActiveAndEnabled && isChasing)
        {
            agent.SetDestination(player.transform.position);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChasing = true;
        }
    }
}
