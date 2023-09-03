using System;

public static class CoinPickUpStaticEvent
{
    public static event Action OnCoinPickUp;

    public static void CallCoinPickUpEvent()
        => OnCoinPickUp?.Invoke();
}
