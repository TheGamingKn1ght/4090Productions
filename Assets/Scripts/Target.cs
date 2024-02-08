using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Death();
        }
    }


    public void Death()
    {
        Destroy(gameObject);
    }
}