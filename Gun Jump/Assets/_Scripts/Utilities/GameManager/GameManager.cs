public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        CurrentGameState = GameState.GameStarted;
    }

    private void OnEnable()
    {
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube += ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
        WeaponFiredStaticEvent.OnWeaponFired += WeaponFiredStaticEvent_OnWeaponFired;
    }

    private void OnDisable()
    {
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
    }

    private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs _)
    {
        CurrentGameState = GameState.PlayingLevel;
        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
    }

    private void ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube(ProjectileHitScoreCubeEventArgs _)
    {
        GameEndedStaticEvent.CallGameEndedEvent();
        CurrentGameState = GameState.GameEnded;
    }
}
