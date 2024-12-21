using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ps && !ps.IsAlive())
            Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
