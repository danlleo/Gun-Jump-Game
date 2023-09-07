using System.Collections;
using _Scripts.Misc;
using UnityEngine;

namespace _Scripts.Camera
{
    public class ScreenShake : Singleton<ScreenShake>
    {
        [SerializeField] public float _shakeDuration = 0.2f; // Duration of the screen shake
        [SerializeField] public float _shakeIntensity = 0.1f; // Intensity of the screen shake

        private Vector3 _originalPosition;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Shake()
        {
            // Save the camera's original position
            _originalPosition = transform.localPosition;

            // Start the coroutine to perform the screen shake
            StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0f;

            // Perform the screen shake for the specified duration
            while (elapsed < _shakeDuration)
            {
                // Generate a random offset for the camera position within the specified intensity
                Vector3 randomOffset = Random.insideUnitSphere * _shakeIntensity;

                // Apply the random offset to the camera's position
                transform.localPosition = _originalPosition + randomOffset;

                // Wait for the next frame
                yield return null;

                elapsed += Time.deltaTime;
            }

            // Reset the camera position to its original position after the shake is finished
            transform.localPosition = _originalPosition;
        }
    }
}
