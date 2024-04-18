using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

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
    [SerializeField] public MeshCollider BodyCollider;

    public static event Action OnEnemyDeath;
    public bool isDead;

    public void TakeDamage(int damage)
    {
        Debug.Log("Damage taken");
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
    public void DealDamage()
    {
        if (HealthBar.health > 0)
        {
            attackNum = RandomNum();
            EnemyAnimator.SetInteger("randomAttackIndex", attackNum);
            AudioManager.Singleton.PlaySoundEffect("Hurt");
            HealthBar.health -= damage;
        }
    }

    public void Death()
    {
        BodyCollider.isTrigger = true;
        this.GetComponent<SphereCollider>().isTrigger = true;
        agent.SetDestination(this.transform.position);
        agent.isStopped = true;
        agent.speed = 0;
        EnemyAnimator.SetBool("isDead", true);
        this.isDead = true;
        StartCoroutine(DestroyBodyTimer());
        OnEnemyDeath?.Invoke();
    }

    IEnumerator DestroyBodyTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        Destroy(this.gameObject);
    }

    private int RandomNum()
    {
        int num = UnityEngine.Random.Range(1,4);
        return num;
    }
}