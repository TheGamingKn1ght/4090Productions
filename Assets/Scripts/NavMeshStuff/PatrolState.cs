using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private NavMeshTracking aiController;

    public PatrolState(NavMeshTracking aiController)
    {
        this.aiController = aiController;
    }

    public void Enter()
    {
        Debug.Log("Animation Patrolling");
    }

    public void Execute()
    {
        aiController.agent.SetDestination(aiController.finalPosition);
        /*
        foreach(Vector3 waypoint in aiController.waypoints)
        {
            aiController.agent.SetDestination(waypoint);
        }
        */
    }

    public void Exit()
    {

    }

}
