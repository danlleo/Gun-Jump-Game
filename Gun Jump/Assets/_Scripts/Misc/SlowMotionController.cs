using System;
using System.Collections;
using _Scripts.Enemy;
using _Scripts.ScoreCubes;
using _Scripts.StaticEvents.Weapon;
using _Scripts.Utilities;
using _Scripts.Utilities.GameManager;
using UnityEngine;

namespace _Scripts.Misc
{
    [DisallowMultipleComponent]
    public class SlowMotionController : Singleton<SlowMotionController>
    {
        private float _originalTimeScale;
        private float _originalFixedDeltaTime;

        private readonly float _slowMotionInDuration = .30f;
        private readonly float _slowMotionOutDuration = .30f;

        private Coroutine _slowMotionRoutine;

        private void Start()
        {
            _originalTimeScale = Time.timeScale;
            _originalFixedDeltaTime = Time.fixedDeltaTime;
        }

        private void OnEnable()
        {   
            WeaponFiredStaticEvent.OnWeaponFired += WeaponFiredStaticEvent_OnWeaponFired;
        }

        private void OnDisable()
        {
            WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;

            ResetTimeScaleAndFixedDeltaTime();
        }
        
        public void TriggerSlowMotion(float duration, float targetTimeScaleValue, Action onSlowMotionStarted, Action onSlowMotionEnded)
        {
            targetTimeScaleValue = Mathf.Clamp(targetTimeScaleValue, 0f, 1f);

            if (_slowMotionRoutine != null)
                return;

            _slowMotionRoutine = StartCoroutine(SlowMotionRoutine(duration, targetTimeScaleValue, onSlowMotionStarted, onSlowMotionEnded));
        }

        private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs weaponFiredEventArgs)
        {
            var ray = new Ray(weaponFiredEventArgs.FiredWeaponShootTransform.position, weaponFiredEventArgs.FiredWeaponShootTransform.forward);

            if (!Physics.Raycast(ray, out RaycastHit hitInfo, 100f)) return;
            if (hitInfo.collider.TryGetComponent(out EnemyHead enemyHead))
            {
                if (!HelperUtilities.IsObjectWithingScreenBoundaries(enemyHead.transform.position))
                    return;
                
                // If we shoot in the enemy's head, put the game in slow motion and disable controls
                TriggerSlowMotion(0.2f, 0.4f, () =>
                    {
                        GameManager.Instance.SetSlowMotionGameState();
                    },
                    (() =>
                    {
                        GameManager.Instance.SetPlayingLevelGameState();
                    }));
                
                return;
            }

            if (!hitInfo.collider.TryGetComponent(out ScoreCube scoreCube)) return;
            if (!HelperUtilities.IsObjectWithingScreenBoundaries(scoreCube.transform.position))
                return;

            // If We hit the Score Cube put the game in slow motion and disable controls
            TriggerSlowMotion(0.2f, 0.1f, () =>
                {
                    GameManager.Instance.SetSlowMotionGameState();
                },
                () => { });
        }

        private IEnumerator SlowMotionRoutine(float duration, float targetTimeScaleValue, Action onSlowMotionStarted = null, Action onSlowMotionEnded = null)
        {
            onSlowMotionStarted?.Invoke();
            // Slow Motion Enter
            float inTimer = 0f;

            while (inTimer <= _slowMotionInDuration)
            {
                inTimer += Time.deltaTime;
                float t = inTimer / _slowMotionInDuration;
                Time.timeScale = Mathf.Lerp(_originalTimeScale, targetTimeScaleValue, t);
                Time.fixedDeltaTime = Time.timeScale * 0.01f;

                yield return null;
            }

            // Slow Motion Stay
            yield return new WaitForSeconds(duration);

            // Slow Motion Leave
            float outTimer = 0f;

            while (outTimer <= _slowMotionOutDuration)
            {
                outTimer += Time.deltaTime;
                float t = outTimer / _slowMotionOutDuration;
                Time.timeScale = Mathf.Lerp(targetTimeScaleValue, _originalTimeScale, t);
                Time.fixedDeltaTime = Time.timeScale * 0.01f;

                yield return null;
            }

            // Ensure that properties are correctly set
            Time.fixedDeltaTime = _originalFixedDeltaTime;
            Time.timeScale = _originalTimeScale;

            _slowMotionRoutine = null;
            onSlowMotionEnded?.Invoke();
        }

        private void ResetTimeScaleAndFixedDeltaTime()
        {
            Time.fixedDeltaTime = _originalFixedDeltaTime;
            Time.timeScale = _originalTimeScale;
        }
    }
}
