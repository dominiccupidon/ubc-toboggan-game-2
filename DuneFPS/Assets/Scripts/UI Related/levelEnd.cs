using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI components
using System.Collections;

public class SceneShifter : MonoBehaviour
{
    public Image fadeImage; // Reference to the UI Image used for fading
    public float fadeDuration = 1f; // Duration of the fade effect

    private void Start()
    {
        // Ensure the fadeImage starts fully transparent
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the fade-out effect and then load the scene
            StartCoroutine(FadeAndLoadScene("Level Select Screen"));
        }
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        if (fadeImage != null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }

        // After the fade-out, load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
