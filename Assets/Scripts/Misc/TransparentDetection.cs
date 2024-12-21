using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparancyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            ComponentCheckSendValue(transparancyAmount);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            ComponentCheckSendValue(1f);
        }
    }

    private void ComponentCheckSendValue(float transparancyValue)
    {
        if (spriteRenderer)
        {
            StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparancyValue));
        }
        else if (tilemap)
        {
            StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparancyValue));
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparancy)
    {
        float elapsedTime = 0;
        Color baseColor = spriteRenderer.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparancy, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparancy)
    {
        float elapsedTime = 0;
        Color baseColor = tilemap.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparancy, elapsedTime / fadeTime);
            tilemap.color = new Color(baseColor.r, baseColor.g, baseColor.b, newAlpha);
            yield return null;
        }
    }
}
