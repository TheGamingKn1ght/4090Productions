using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTracking : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Transform enemyEyes;
    [SerializeField] public Transform target;
    [SerializeField] private int maxWalkRadius = 10;
    
    
    private StateMachine stateMachine;
    public Vector3 waypoint;
    public Vector3 finalPosition;
    public bool CanSeePlayer;
    

    public void Start()
    {
        stateMachine = new StateMachine();
        finalPosition = GetWaypoint();
        stateMachine.changeState(new PatrolState(this,finalPosition));

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
            agent.GetComponent<Enemy>().DealDamage(enemyEyes);
        }

        if (CanSeePlayer == true)
        {
            stateMachine.changeState(new ChaseState(this));
        }
        else
        {
            if(transform.position == finalPosition)
            {
                finalPosition = GetWaypoint();
                stateMachine.changeState(new PatrolState(this, finalPosition));
            }
            else
            {
                //stateMachine.currentState.Execute();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanSeePlayer = false;
            //Recommence the Random pathfinding after leaving chase state
            //Would like to do this differently
            stateMachine.changeState(new PatrolState(this, finalPosition));
        }
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }


    public Vector3 GetWaypoint()
    {
        
        waypoint = Random.insideUnitSphere * maxWalkRadius;
        waypoint += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(waypoint, out hit, maxWalkRadius, 1);
        return hit.position;
        /*
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxWalkRadius;
            waypoints[i] = randomDirection;
            waypoints[i] += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(waypoints[i], out hit, maxWalkRadius, 1);
            finalPosition = hit.position;
        }
        */
    }
}
