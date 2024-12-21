using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<EnemyHealth>()?.TakeDamage(damageAmount);
    }
}
