using System;
using System.Collections;
using _Scripts.StaticEvents.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Enemy
{
    [SelectionBase]
    [RequireComponent(typeof(EnemyHitEvent))]
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        [HideInInspector] public EnemyHitEvent EnemyHitEvent;

        [Tooltip("Populate with the collider responsible to detect body shots")]
        [SerializeField] private CapsuleCollider _enemyBodyCapsuleCollider;

        [Tooltip("Populate with the collider responsible to detect headshots")]
        [SerializeField] private SphereCollider _enemyHeadSphereCollider;
    
        [Tooltip("Populate with the material that will represent enemy's death state using skinned mesh renderer")]
        [SerializeField] private Material _enemyDeadMaterial;

        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

        [FormerlySerializedAs("_dissolveTime")]
        [Tooltip("Set time which will take enemy to dissolve")]
        [SerializeField] private float _dissolveTimeInSeconds = 1f;
        
        [FormerlySerializedAs("_rb")]
        [Tooltip(
            "Populate with a ragdoll rigidbody to apply force in the future from projectiles or explosion barrels")]
        
        public Rigidbody Rb;
        
        private Animator _animator;

        private MaterialPropertyBlock _materialPropertyBlock;
        
        private bool _isDead;
        private static readonly int Opacity = Shader.PropertyToID("_Opacity");

        private void Awake()
        {
            EnemyHitEvent = GetComponent<EnemyHitEvent>();
            _animator = GetComponent<Animator>();
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        private void OnEnable()
        {
            EnemyHitEvent.OnEnemyHit += EnemyHitEvent_OnEnemyHit;
        }

        private void OnDisable()
        {
            EnemyHitEvent.OnEnemyHit -= EnemyHitEvent_OnEnemyHit;
        }

        private void EnemyHitEvent_OnEnemyHit(EnemyHitEvent enemyHitEvent, EnemyHitEventArgs enemyHitEventArgs)
        {
            if (_isDead) 
                return;

            _isDead = true;

            if (enemyHitEventArgs.IsHeadshot)
            {
                EnemyDiedStaticEvent.CallEnemyDiedEvent(transform.position, true);
                Economy.Economy.AddMoneyForKillingEnemy(true);
            }
            else
            {
                EnemyDiedStaticEvent.CallEnemyDiedEvent(transform.position);
                Economy.Economy.AddMoneyForKillingEnemy(false);
            }
            
            SetEnemyDeadMaterial();
            DisableAnimator();
            StartCoroutine(DissolveRoutine(() => Destroy(gameObject)));
            
            _enemyBodyCapsuleCollider.enabled = false;
            _enemyHeadSphereCollider.enabled = false;
        }

        private void SetEnemyDeadMaterial()
            => _skinnedMeshRenderer.material = _enemyDeadMaterial;

        private void DisableAnimator()
            => _animator.enabled = false;

        private IEnumerator DissolveRoutine(Action onComplete)
        {
            float timer = 0f;

            while (timer <= _dissolveTimeInSeconds)
            {
                timer += Time.deltaTime;
                
                float t = timer / _dissolveTimeInSeconds;
                
                _materialPropertyBlock.SetFloat(Opacity, t);
                _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
                
                yield return null;
            }
            
            onComplete?.Invoke();
        }
    }
}
