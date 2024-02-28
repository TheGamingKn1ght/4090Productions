using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTracking : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    private void Update()
    {
        if(agent.GetComponent<NavMeshAgent>().isActiveAndEnabled)
        {
            agent.SetDestination(player.transform.position);
        }
        
    }
}
