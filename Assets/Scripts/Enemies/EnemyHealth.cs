using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 50;

    private int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
