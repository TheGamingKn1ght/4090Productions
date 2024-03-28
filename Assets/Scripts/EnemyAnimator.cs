using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    //[SerializeField] private NavMeshAgent agent;

    private void LateUpdate()
    {
        UpdateLocalAnimator();
    }

    private void UpdateLocalAnimator()
    {
        //animator.SetFloat("speedX", agent.speed);
    }
}
