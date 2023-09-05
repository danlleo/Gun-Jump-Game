using System;

public static class EconomyMadePurchaseStaticEvent
{
    public static event Action OnMadePurchase;

    public static void CallMadePurchaseEvent()
        => OnMadePurchase?.Invoke();
}
