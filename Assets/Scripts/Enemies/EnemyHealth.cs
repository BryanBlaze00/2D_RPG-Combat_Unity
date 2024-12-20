using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 50;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
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
        flash.FlashRoutine();
        StartCoroutine(WaitAndDetectDeath());
    }

    private IEnumerator WaitAndDetectDeath()
    {
        yield return new WaitForSeconds(flash.WhiteFlashDuration * 2);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
