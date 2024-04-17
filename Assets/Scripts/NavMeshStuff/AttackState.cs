using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    private NavMeshTracking aiController;

    public AttackState(NavMeshTracking aiController)
    {
        this.aiController = aiController;
    }

    public void Enter()
    {
        Debug.Log("Animation Attacking");
        aiController.agent.speed = 0;
        aiController.GetComponent<Enemy>().EnemyAnimator.SetBool("isAttacking", true);
    }

    public void Execute()
    {
        //aiController.agent.GetComponent<Enemy>().DealDamage();
    }

    public void Exit()
    {
        aiController.agent.speed = 3;
        aiController.GetComponent<Enemy>().EnemyAnimator.SetBool("isAttacking", false);
    }

}
