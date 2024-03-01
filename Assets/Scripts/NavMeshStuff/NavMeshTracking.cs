using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTracking : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform target;
    [SerializeField] private int maxWalkRadius = 10;
    
    
    private StateMachine stateMachine;
    public Vector3 waypoints;// = new Vector3[5];
    public Vector3 finalPosition;
    public static bool CanSeePlayer;
    

    public void Start()
    {
        stateMachine = new StateMachine();
        GetWaypoints();
        stateMachine.changeState(new PatrolState(this));

        agent.velocity = Vector3.zero;
    }

    private void Update()
    {
        stateMachine.ExecuteState();
        HandleTransition();
    }

    public void HandleTransition()
    {
        if(stateMachine.currentState is ChaseState)
        {
            //if close enough, attack state
        }

        if (CanSeePlayer)
        {
            stateMachine.changeState(new ChaseState(this));
        }
        else
        {
            /*
            for(int i=0; i <5; i++)
            {
                waypoints = Random.insideUnitSphere * maxWalkRadius;
            }
            */

            Debug.Log(waypoints);
            stateMachine.changeState(new PatrolState(this));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanSeePlayer = true;
        }
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }


    public void GetWaypoints()
    {
        waypoints = Random.insideUnitSphere * maxWalkRadius;
        waypoints += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(waypoints, out hit, maxWalkRadius, 1);
        finalPosition = hit.position;
    }
}
