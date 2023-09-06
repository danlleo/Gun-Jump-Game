using System;

namespace _Scripts.StaticEvents.GameManager
{
    public static class GameEndedStaticEvent
    {
        public static event Action OnGameEnded;

        public static void CallGameEndedEvent()
            => OnGameEnded?.Invoke();
    }
}
