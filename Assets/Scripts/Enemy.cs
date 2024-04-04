using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Agent Properties")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float damage;
    [SerializeField] private float health = 100;

    [Header("Attack Properties")]
    [SerializeField] private float AttackWaitTime; 
    private float currentAttackWaitTime = 0f;
    private bool isWaiting;

    [SerializeField] public Animator EnemyAnimator;

    public float impactForce = 500;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
        else
        {
            EnemyAnimator.SetBool("isHit", true);
        }
    }

    public void TakeDamage(RaycastHit info, Transform camPos, int damage)
    {
        if (info.rigidbody.CompareTag("Enemy") == true)
        {
            health -= damage;
            if(health <= 0)
            {
                //Death(info);
            }
            else
            {
                EnemyAnimator.SetBool("isHit", true);
            }
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
                EnemyAnimator.SetBool("isAttacking", true);
                currentAttackWaitTime = 0f;
                AudioManager.Singleton.PlaySoundEffect("Crowbar");
                
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

    public void Death()
    {
        //agent.enabled = false;
        agent.SetDestination(this.transform.position);
        agent.isStopped = true;
        agent.speed = 0;
        EnemyAnimator.SetBool("isDead", true);
        //character.transform.Translate(Vector3.up);
    }


}