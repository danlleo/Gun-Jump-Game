using UnityEngine;

namespace _Scripts.Weapons
{
    [CreateAssetMenu(fileName = "WeaponDetails_", menuName = "Scriptable Objects/Weapons/WeaponDetails")]
    public class WeaponDetailsSO : ScriptableObject
    {
        #region VALUES THAT ARE ASSIGNED IN THE INSPECTOR

        #region WEAPON PROPERTIES

        [Space(10)]
        [Header("Weapon Properties")]
        [Space(5)]

        [SerializeField] private float _maxAngularVelocity;
        [SerializeField] private float _torque;
        [SerializeField] private float _maxTorqueBonus;
        [SerializeField] private float _maxJumpHeight;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _sideForce;
        [SerializeField] private float _bounceBackForce;

        #endregion

        #region WEAPON'S PROJECTILE PROPERTIES

        [Space(10)]
        [Header("Weapon's Projectile Properties")]
        [Space(5)]

        [SerializeField] private bool _projectilesCanRicochet;
        [SerializeField] private bool _projectilesCanGoTroughBodies;

        #endregion

        #endregion

        #region REFERENCE VALUES

        public float MaxAngularVelocity => _maxAngularVelocity;
        public float Torque => _torque;
        public float MaxTorqueBonus => _maxTorqueBonus;
        public float MaxJumpHeight => _maxJumpHeight;
        public float JumpForce => _jumpForce;
        public float SideForce => _sideForce;
        public float BounceBackForce => _bounceBackForce;
        public bool ProjectilesCanRicochet => _projectilesCanRicochet;
        public bool ProjectilesCanGoTroughBodies => _projectilesCanGoTroughBodies;

        #endregion

    }
}
