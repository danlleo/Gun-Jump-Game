using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }
    public SaveData SaveGameData { get; private set; }

    public int CurrentLevel { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SaveGameData = SaveLoaderController.Load();
        CurrentGameState = GameState.GameEnded;
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
    }

    private void OnDisable()
    {
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
        GameEndedStaticEvent.OnGameEnded -= GameEndedStaticEvent_OnGameEnded;
        EconomyMadePurchaseStaticEvent.OnMadePurchase -= EconomyMadePurchaseStaticEvent_OnMadePurchase;
    }

    private void EconomyMadePurchaseStaticEvent_OnMadePurchase()
    {
        SaveLoaderController.Save(SaveGameData);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SaveLoaderController.DeleteSave();
    }
#endif

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
}
