using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    public int startingHealth = 50;
    public int threshold = 25;

    public int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void Damage(int damage, Vector3 hitPoint)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public int Health()
    {
        return currentHealth;
    }

    public int Threshold()
    {
        return threshold;
    }

    public int MaxHealth()
    {
        return startingHealth;
    }

    public void SetNextThreshold(int i)
    {
        threshold = i;
    }
	
    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
