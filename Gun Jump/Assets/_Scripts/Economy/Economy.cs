using System;

public static class Economy
{
    public static int CurrentLevelMoneyAmount { private set; get; }
    public static int TotalMoneyAmountBeforeBeatingLevel { private set; get; }
    public static int TotalMoneyAmount { private set; get; }

    public static readonly int DefaultMoneyAmountToAdd = 100;

    public static void CalculateReceivedMoneyFromScoreCubeAndAddToCurrentAmount(int bonusEarningAmount)
    {
        if (bonusEarningAmount <= 0)
            throw new ArgumentException("Bonus Earning Amount cannot be less or equal to zero");

        TotalMoneyAmount += DefaultMoneyAmountToAdd + CurrentLevelMoneyAmount * bonusEarningAmount;
    }

    public static void AddMoneyForKillingEnemy(bool hasDiedOutOfHeadshot)
    {
        if (hasDiedOutOfHeadshot) CurrentLevelMoneyAmount += 600;
        else CurrentLevelMoneyAmount += 400;
    }

    public static void CleanCurrentLevelMoneyAmount()
        => CurrentLevelMoneyAmount = 0;
}
