using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private NavMeshTracking aiController;
    private Vector3 patrolPoint;

    public PatrolState(NavMeshTracking aiController, Vector3 patrolPoint)
    {
        this.aiController = aiController;
        this.patrolPoint = patrolPoint;
    }

    public void Enter()
    {
        Debug.Log("Animation Patrolling");
        aiController.agent.speed = 1;
        aiController.agent.SetDestination(patrolPoint);
    }

    public void Execute()
    {
        /*
        foreach(Vector3 waypoint in aiController.waypoints)
        {
            if(aiController.agent.transform.position != aiController.finalPosition)
            {
                aiController.agent.SetDestination(aiController.finalPosition);
            }
        }
        */
        
    }

    public void Exit()
    {

    }

}
