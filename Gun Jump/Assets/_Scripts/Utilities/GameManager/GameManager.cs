using System.Collections;
using _Scripts.Enums;
using _Scripts.Misc;
using _Scripts.Save;
using _Scripts.StaticEvents.Economy;
using _Scripts.StaticEvents.GameManager;
using _Scripts.StaticEvents.ScoreCube;
using _Scripts.StaticEvents.Weapon;
using _Scripts.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Utilities.GameManager
{
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentGameState { get; private set; }
        public SaveData SaveGameData { get; private set; }

        public int CurrentLevel { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SaveGameData = SaveLoaderController.Load();
            CurrentGameState = GameState.GAME_STARTED;
            CurrentLevel = SaveGameData.CurrentLevel;

            Economy.Economy.CleanCurrentLevelMoneyAmount();
            Economy.Economy.SetTotalMoneyAmountBeforeBeatingLevel(SaveGameData.MoneyAmount);
            Economy.Economy.SetTotalMoneyAmount(SaveGameData.MoneyAmount);
        }

        private void OnEnable()
        {
            ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube += ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
            WeaponFiredStaticEvent.OnWeaponFired += WeaponFiredStaticEvent_OnWeaponFired;
            GameEndedStaticEvent.OnGameEnded += GameEndedStaticEvent_OnGameEnded;
            EconomyMadePurchaseStaticEvent.OnMadePurchase += EconomyMadePurchaseStaticEvent_OnMadePurchase;
            WeaponFallingStaticEvent.OnWeaponFalling += WeaponFallingStaticEvent_OnWeaponFalling;
        }

        private void OnDisable()
        {
            ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
            WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
            GameEndedStaticEvent.OnGameEnded -= GameEndedStaticEvent_OnGameEnded;
            EconomyMadePurchaseStaticEvent.OnMadePurchase -= EconomyMadePurchaseStaticEvent_OnMadePurchase;
            WeaponFallingStaticEvent.OnWeaponFalling -= WeaponFallingStaticEvent_OnWeaponFalling;
        }

        private void WeaponFallingStaticEvent_OnWeaponFalling()
        {
            if (CurrentGameState == GameState.GAME_ENDED)
                return;
            
            StartCoroutine(ReloadCurrentSceneAfterDelayRoutine(1.75f));
        }

        private void EconomyMadePurchaseStaticEvent_OnMadePurchase()
        {
            SaveLoaderController.Save(SaveGameData);
        }

        private void Update()
        {
            if (!PlayerInputHandler.IsMouseButtonDownThisFrame()) return;
            if (CurrentGameState == GameState.GAME_STARTED)
                CurrentGameState = GameState.PLAYING_LEVEL;
        }

        public void SaveSelectedWeapon(WeaponSO weapon)
        {
            SaveGameData.SelectedWeaponID = weapon.WeaponID;
            SaveLoaderController.Save(SaveGameData);
        }
        
        public void SetSlowMotionGameState()
            => CurrentGameState = GameState.IN_SLOW_MOTION;

        public void SetPlayingLevelGameState()
            => CurrentGameState = GameState.PLAYING_LEVEL;
        
        private void GameEndedStaticEvent_OnGameEnded()
        {
            CurrentGameState = GameState.GAME_ENDED;
            IncreaseCurrentLevel();

            SaveGameData.MoneyAmount = Economy.Economy.TotalMoneyAmount;

            SaveLoaderController.Save(SaveGameData);
        }

        private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs _)
        {
            CurrentGameState = GameState.PLAYING_LEVEL;
            WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
        }

        private void ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube(ProjectileHitScoreCubeEventArgs _)
        {
            GameEndedStaticEvent.CallGameEndedEvent();
        }

        private void IncreaseCurrentLevel()
        {
            CurrentLevel++;
            SaveGameData.CurrentLevel = CurrentLevel;
        }

        private IEnumerator ReloadCurrentSceneAfterDelayRoutine(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
