using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Agent Properties")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float damage;

    [Header("Attack Properties")]
    [SerializeField] private float AttackWaitTime; 
    private float currentAttackWaitTime = 0f;
    private bool isWaiting;

    [SerializeField] Animator EnemyAnimator;

    public float impactForce = 500;

    public void TakeDamage(RaycastHit info, Transform camPos)
    {
        if (info.rigidbody.CompareTag("Enemy") == true)
        {
            Death(info);
        }
        else
        {
            //AudioManager.Singleton.PlaySoundEffect("Riccochet");
            Vector3 direction = info.transform.position - camPos.transform.position;
            info.rigidbody.AddForce(direction * impactForce);
        }
        
    }

    public void DealDamage(Transform camPos, Transform player)
    {
        if (isWaiting)
        {
            currentAttackWaitTime += Time.deltaTime;
            if(currentAttackWaitTime >= AttackWaitTime)
            {
                isWaiting = false;
                currentAttackWaitTime = 0f;
            }
        }
        else
        {
            if(HealthBar.health > 0)
            {
                AudioManager.Singleton.PlaySoundEffect("Oof");
                HealthBar.health -= damage;
                isWaiting = true;
            }
            
        }
        /*
        RaycastHit hit;
        if(Physics.Raycast(camPos.transform.position, player.transform.position, out hit, 2))
        {
            
        }
        */
    }

    public void Death(RaycastHit character)
    {
        //agent.enabled = false;
        agent.SetDestination(this.transform.position);
        agent.isStopped = true;
        agent.speed = 0;
        character.transform.Rotate(-Vector3.right,90);
        EnemyAnimator.SetBool("isDead", true);
        //character.transform.Translate(Vector3.up);
    }


}