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
    Dictionary<int, int> attackWaitTimes = new Dictionary<int, int>();
    private float AttackWaitTime;
    private float currentAttackWaitTime = 0f;
    private bool isWaiting;
    private int attackNum = 0;

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
            AudioManager.Singleton.PlaySoundEffect("ZombieHit");
            //trying to reset
            EnemyAnimator.SetBool("isHit", false);
        }
    }
    /*
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
    */
    public void DealDamage(Transform camPos, Transform player)
    {
        if(attackNum == 0)
        {
            attackNum = RandomNum();
        }
        if (isWaiting)
        {
            switch (attackNum)
            {
                case 1:
                    AttackWaitTime = 1.017f;
                    break;
                case 2:
                    AttackWaitTime = 3.367f;
                    break;
                case 3:
                    AttackWaitTime = 4.617f;
                    break;
            }
            Debug.Log("Wait: " + AttackWaitTime);
            /*
            foreach(KeyValuePair<int, int> vals in attackWaitTimes)
            {
                if(attackNum == vals.Key)
                {
                    attackWaitTimes = vals.Key;
                }
            }
            */
            currentAttackWaitTime += Time.deltaTime;
            if(currentAttackWaitTime >= AttackWaitTime)
            {
                isWaiting = false;
                EnemyAnimator.SetBool("isAttacking", true);
                EnemyAnimator.SetInteger("randomAttackIndex", attackNum);
                attackNum = 0;
                currentAttackWaitTime = 0f;
                AudioManager.Singleton.PlaySoundEffect("ZombieAttack");
                
            }
        }
        else
        {
            if(HealthBar.health > 0)
            {
                AudioManager.Singleton.PlaySoundEffect("Hurt");
                HealthBar.health -= damage;
                isWaiting = true;
            }
            
        }
    }

    public void Death()
    {
        agent.SetDestination(this.transform.position);
        agent.isStopped = true;
        agent.speed = 0;
        EnemyAnimator.SetBool("isDead", true);
    }

    private int RandomNum()
    {
        int num = Random.Range(0,4);
        return num;
    }
}