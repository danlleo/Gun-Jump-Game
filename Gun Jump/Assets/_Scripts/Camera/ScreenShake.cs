using System.Collections;
using UnityEngine;

public class ScreenShake : Singleton<ScreenShake>
{
    public float shakeDuration = 0.2f; // Duration of the screen shake
    public float shakeIntensity = 0.1f; // Intensity of the screen shake

    private Vector3 originalPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Shake()
    {
        // Save the camera's original position
        originalPosition = transform.localPosition;

        // Start the coroutine to perform the screen shake
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        // Perform the screen shake for the specified duration
        while (elapsed < shakeDuration)
        {
            // Generate a random offset for the camera position within the specified intensity
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;

            // Apply the random offset to the camera's position
            transform.localPosition = originalPosition + randomOffset;

            // Wait for the next frame
            yield return null;

            elapsed += Time.deltaTime;
        }

        // Reset the camera position to its original position after the shake is finished
        transform.localPosition = originalPosition;
    }
}
