using System;

public static class EarningManager
{
    public static event EventHandler<EarningManagerEventArgs> OnReceivedMoney;

    private static int _defaultMoneyAmountToAdd = 100;
    private static int _currentMoneyAmount;

    public static int CalculateReceivedMoneyFromScoreCube(int bonusEarningAmount)
    {
        if (bonusEarningAmount <= 0)
            throw new ArgumentException("Bonus Earning Amount cannot be less or equal to zero");

        _currentMoneyAmount += _defaultMoneyAmountToAdd * bonusEarningAmount;
        OnReceivedMoney?.Invoke(null, new EarningManagerEventArgs(_currentMoneyAmount));

        return _defaultMoneyAmountToAdd * bonusEarningAmount;
    }
}
