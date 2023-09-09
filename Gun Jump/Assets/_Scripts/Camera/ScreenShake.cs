using _Scripts.Misc;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Camera
{
    public class ScreenShake : Singleton<ScreenShake>
    {
        [SerializeField] private float _shakeIntensity = 1f; // Intensity of the screen shake
        [SerializeField] private float _shakeDuration = 0.2f; // Duration of the screen shake

        private float _timer;
        
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        protected override void Awake()
        {
            base.Awake();
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            StopShake();
        }

        public void ShakeCamera()
        {
            var cinemachineBasicMultiChannelPerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _shakeIntensity;

            _timer = _shakeDuration;
        }

        public void ShakeCamera(float shakeIntensity, float shakeDuration)
        {
            var cinemachineBasicMultiChannelPerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;

            _timer = shakeDuration;
        }

        private void StopShake()
        {
            var cinemachineBasicMultiChannelPerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

            _timer = 0f;
        }

        private void Update()
        {
            if (!(_timer > 0)) return;
            
            _timer -= Time.deltaTime;
                
            if (_timer <= 0f)
                StopShake();
        }
    }
}
