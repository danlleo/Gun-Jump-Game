using UnityEngine;

namespace _Scripts.Environment.Clouds
{
    public class Cloud : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _timeToMoveToOneSideInSeconds;

        private float _timer;

        private bool _movingForward;
    
        private void Update()
            => MoveCloud();

        private void MoveCloud()
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeToMoveToOneSideInSeconds)
            {
                _movingForward = !_movingForward;

                ResetTimer();
            }
        
            if (_movingForward)
                transform.Translate(Vector3.forward * (_moveSpeed * Time.deltaTime));
            else
                transform.Translate(Vector3.back * (_moveSpeed * Time.deltaTime));
        }

        private void ResetTimer()
            => _timer = 0f;
    }
}
