using System.Collections;
using UnityEngine;

public class DeletionScript : MonoBehaviour
{
    public float fadeDuration = 5.0f; // Duration of the fade-out effect
    private Material material;
    private Color startColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            startColor = material.color;
        }
        else
        {
            Debug.LogError("No Renderer component found.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        if (material == null) yield break; // Exit if material is not set

        Color color = startColor;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            color.a = alpha;
            material.color = color;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure final alpha value is 0
        color.a = 0f;
        material.color = color;

        Destroy(gameObject); // Destroy the object after the fade-out
    }
}
