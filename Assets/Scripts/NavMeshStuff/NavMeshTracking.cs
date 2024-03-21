using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTracking : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform enemyEyes;
    [SerializeField] public Transform target;
    [SerializeField] private int maxWalkRadius = 10;
    [SerializeField] private SphereCollider EnemySphereCollider;
    [SerializeField] private CapsuleCollider CloseUpCollider;
    
    
    private StateMachine stateMachine;
    public Vector3 waypoint;
    public Vector3 finalPosition;
    public bool canSeePlayer;
    public bool canAttackPlayer;
    

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
            if (canAttackPlayer == true)
            {
                stateMachine.changeState(new AttackState(this));
            }
        }

        if (canSeePlayer == true)
        {
            stateMachine.changeState(new ChaseState(this));
        }
        else if (stateMachine.currentState is ChaseState)
        {
            if (canAttackPlayer == true)
            {
                stateMachine.changeState(new AttackState(this));
            }
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
            canSeePlayer = true;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            //canAttackPlayer = true;
        }
    }

    private void OnTriggerExit()
    {
        if (EnemySphereCollider.gameObject.CompareTag("Player"))
        {
            canSeePlayer = false;
            //Recommence the Random pathfinding after leaving chase state
            //Would like to do this differently
            stateMachine.changeState(new PatrolState(this, finalPosition));
        }
        if (CloseUpCollider.gameObject.CompareTag("Player"))
        {
            canAttackPlayer = false;
            //Recommence the Random pathfinding after leaving chase state
            //Would like to do this differently
            stateMachine.changeState(new ChaseState(this));
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
