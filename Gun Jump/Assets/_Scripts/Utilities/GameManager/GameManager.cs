public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }

    public int CurrentLevel { get; private set; }

    private SaveData _saveData;

    protected override void Awake()
    {
        base.Awake();

        _saveData = SaveLoaderController.Load();
        CurrentGameState = GameState.GameEnded;
        CurrentLevel = _saveData.CurrentLevel;
    }

    private void OnEnable()
    {
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube += ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
        WeaponFiredStaticEvent.OnWeaponFired += WeaponFiredStaticEvent_OnWeaponFired;
        GameEndedStaticEvent.OnGameEnded += GameEndedStaticEvent_OnGameEnded;
    }

    private void OnDisable()
    {
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
        GameEndedStaticEvent.OnGameEnded -= GameEndedStaticEvent_OnGameEnded;
    }

    private void GameEndedStaticEvent_OnGameEnded()
    {
        CurrentGameState = GameState.GameEnded;
    }

    private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs _)
    {
        CurrentGameState = GameState.PlayingLevel;
        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
    }

    private void ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube(ProjectileHitScoreCubeEventArgs _)
    {
        IncreaseCurrentLevel();
        GameEndedStaticEvent.CallGameEndedEvent();
    }

    private void IncreaseCurrentLevel()
    {
        CurrentLevel++;
        _saveData.CurrentLevel = CurrentLevel;
    }
}
