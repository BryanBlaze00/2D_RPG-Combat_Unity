using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DamageSource>()) 
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
