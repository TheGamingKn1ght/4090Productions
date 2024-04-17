using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTracking : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform enemyEyes;
    [SerializeField] private int maxWalkRadius = 10;
    [SerializeField] private SphereCollider EnemySphereCollider;
    [SerializeField] private CapsuleCollider CloseUpCollider;
    public Transform target;

    [SerializeField] private float attackDistanceMargin;

    private StateMachine stateMachine;
    public Vector3 waypoint;
    public Vector3 finalPosition;
    public bool canSeePlayer;
    private float attackDistance;
    private float positionDistance;


    public void Start()
    {
        stateMachine = new StateMachine();
        target = FindAnyObjectByType<PlayerController>().transform;
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
        
        if (canSeePlayer == true && stateMachine.currentState is PatrolState)
        {
            stateMachine.changeState(new ChaseState(this));
            Debug.Log(stateMachine.currentState);
        }
        
        if (stateMachine.currentState is ChaseState)
        {
            attackDistance = Vector3.Distance(agent.transform.position, target.transform.position);
            if (attackDistance <= attackDistanceMargin)
            {
                //Debug.Log(attackDistance);
                stateMachine.changeState(new AttackState(this));
                Debug.Log(stateMachine.currentState);
            }
        }
        else if (stateMachine.currentState is AttackState)
        {
            attackDistance = Vector3.Distance(agent.transform.position, target.transform.position);
            if (attackDistance > attackDistanceMargin+0.5)
            {
                stateMachine.changeState(new ChaseState(this));
                Debug.Log(stateMachine.currentState);
            }
        }
        else if(stateMachine.currentState is PatrolState)
        {
            positionDistance = Vector3.Distance(agent.transform.position,finalPosition);
            if (positionDistance <= 0.6)
            {
                finalPosition = GetWaypoint();
                stateMachine.changeState(new PatrolState(this, finalPosition));
                Debug.Log(stateMachine.currentState);
            }
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSeePlayer = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Switching to Patrol");
            canSeePlayer = false;
            //Recommence the Random pathfinding after leaving chase state
            //Would like to do this differently
            finalPosition = GetWaypoint();
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
    }
}
