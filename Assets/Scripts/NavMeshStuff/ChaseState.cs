using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private NavMeshTracking aiController;

    public ChaseState(NavMeshTracking aiController)
    {
        this.aiController = aiController;
    }

    public void Enter()
    {
        Debug.Log("Animation Chasing");
    }

    public void Execute()
    {
        //Chasing code
        if (aiController.agent.GetComponent<NavMeshAgent>().isActiveAndEnabled && NavMeshTracking.CanSeePlayer)
        {
            aiController.agent.SetDestination(aiController.target.transform.position);
        }
    }

    public void Exit()
    {

    }

}
