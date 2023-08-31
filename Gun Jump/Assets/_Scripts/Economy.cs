using System;

public static class Economy
{
    public static event EventHandler<EarningManagerEventArgs> OnReceivedMoney;

    private static int s_defaultMoneyAmountToAdd = 100;
    private static int s_currentMoneyAmount;

    public static int CalculateReceivedMoneyFromScoreCube(int bonusEarningAmount)
    {
        if (bonusEarningAmount <= 0)
            throw new ArgumentException("Bonus Earning Amount cannot be less or equal to zero");

        s_currentMoneyAmount += s_defaultMoneyAmountToAdd * bonusEarningAmount;
        OnReceivedMoney?.Invoke(null, new EarningManagerEventArgs(s_currentMoneyAmount));

        return s_defaultMoneyAmountToAdd * bonusEarningAmount;
    }
}
