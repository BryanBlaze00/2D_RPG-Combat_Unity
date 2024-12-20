using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 50;

    private int currentHealth;
    private Knockback knockback;

    private void Awake() {
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(Player.Instance.transform, Player.Instance.GetKnockbackThrustAmount);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
