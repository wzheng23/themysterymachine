using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    public int startingHealth = 5;

    private int currentHealth;

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
	
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
