using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    int maxHealth = 100;
    int currentHealth;

	void Start ()
    {
        currentHealth = maxHealth;
	}
	

	void Update ()
    {
        if (currentHealth < 0)
            Die();
	}

    void Die()
    {
        print("Dead");
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
    }
}
