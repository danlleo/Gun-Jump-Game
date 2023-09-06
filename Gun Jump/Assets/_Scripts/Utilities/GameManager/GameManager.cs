using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }
    public SaveData SaveGameData { get; private set; }

    public int CurrentLevel { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SaveGameData = SaveLoaderController.Load();
        CurrentGameState = GameState.GameStarted;
        CurrentLevel = SaveGameData.CurrentLevel;

        Economy.CleanCurrentLevelMoneyAmount();
        Economy.SetTotalMoneyAmount(SaveGameData.MoneyAmount);
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
        StartCoroutine(ReloadCurrentSceneAfterDelayRoutine(1.75f));
    }

    private void EconomyMadePurchaseStaticEvent_OnMadePurchase()
    {
        SaveLoaderController.Save(SaveGameData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SaveLoaderController.DeleteSave();

        if (PlayerInputHandler.IsMouseButtonDownThisFrame())
        {
            if (CurrentGameState == GameState.GameStarted)
                CurrentGameState = GameState.PlayingLevel;
        }
    }

    private void GameEndedStaticEvent_OnGameEnded()
    {
        CurrentGameState = GameState.GameEnded;
        IncreaseCurrentLevel();

        SaveGameData.MoneyAmount = Economy.TotalMoneyAmount;

        SaveLoaderController.Save(SaveGameData);
    }

    private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs _)
    {
        CurrentGameState = GameState.PlayingLevel;
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
