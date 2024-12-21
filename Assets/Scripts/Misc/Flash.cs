using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float redFlashDuration = 0.1f;
    [SerializeField] private float whiteFlashDuration = 0.2f;
    public float WhiteFlashDuration { get => whiteFlashDuration; }

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public void FlashRoutine()
    {
        StartCoroutine(FlashRedRoutine());
    }

    private IEnumerator FlashRedRoutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(redFlashDuration);
        spriteRenderer.color = Color.white;
        StartCoroutine(FlashWhiteRoutine());
    }

    private IEnumerator FlashWhiteRoutine()
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(whiteFlashDuration);
        spriteRenderer.material = defaultMat;
    }
}
