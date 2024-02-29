using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTracking : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;


    private StateMachine stateMachine;
    private bool isChasing;

    public void Start()
    {
        stateMachine = new StateMachine();
        //stateMachine.changeState();

        agent.velocity = Vector3.zero;
    }

    private void Update()
    {
        //Chasing code
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
