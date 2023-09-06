using System;

namespace _Scripts.StaticEvents.Coin
{
    public static class CoinPickUpStaticEvent
    {
        public static event Action OnCoinPickUp;

        public static void CallCoinPickUpEvent()
            => OnCoinPickUp?.Invoke();
    }
}
