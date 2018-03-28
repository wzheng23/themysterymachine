using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*Inherits IDamageable interface*/
public class EnemyHealth : MonoBehaviour, IDamageable {

    public int startingHealth = 20;
    public int threshold = 10;

    private int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void Damage(int damage, Vector3 hitPoint)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)     //kills enemy if health reaches zero
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
	
    public void Dead()      //Currently Dead, just disables the enemy object
    {
        gameObject.SetActive(false);
    }
}
