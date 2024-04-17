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
        aiController.agent.speed = 3;
        aiController.GetComponent<Enemy>().EnemyAnimator.SetBool("isChasing", true);
        //aiController.GetComponent<AudioSource>().clip = AudioManager.Singleton.soundEffects[11].audioClip;
    }

    public void Execute()
    {
        //Chasing code
        if (aiController.canSeePlayer)
        {
            //Chasing
            aiController.agent.SetDestination(aiController.target.transform.position);
        }
    }

    public void Exit()
    {
        aiController.GetComponent<Enemy>().EnemyAnimator.SetBool("isChasing", false);
        //aiController.GetComponent<AudioSource>().clip = AudioManager.Singleton.soundEffects[12].audioClip;
    }

}
