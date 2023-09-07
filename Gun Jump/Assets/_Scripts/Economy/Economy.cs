using System;

namespace _Scripts.Economy
{
    public static class Economy
    {
        public static int CurrentLevelMoneyAmount { private set; get; }
        public static int TotalMoneyAmountBeforeBeatingLevel { private set; get; }
        public static int TotalMoneyAmount { private set; get; }

        public const int DEFAULT_MONEY_AMOUNT_TO_ADD = 100;

        public static void CalculateReceivedMoneyFromScoreCubeAndAddToCurrentAmount(int bonusEarningAmount)
        {
            if (bonusEarningAmount <= 0)
                throw new ArgumentException("Bonus Earning Amount cannot be less or equal to zero");

            CurrentLevelMoneyAmount += DEFAULT_MONEY_AMOUNT_TO_ADD;
            TotalMoneyAmount += CurrentLevelMoneyAmount * bonusEarningAmount;
        }

        public static void AddMoneyForKillingEnemy(bool hasDiedOutOfHeadshot)
        {
            if (hasDiedOutOfHeadshot) CurrentLevelMoneyAmount += 600;
            else CurrentLevelMoneyAmount += 400;
        }

        public static void CleanCurrentLevelMoneyAmount()
            => CurrentLevelMoneyAmount = 0;

        public static void SetTotalMoneyAmountBeforeBeatingLevel(int moneyAmount)
        {
            if (moneyAmount < 0)
                throw new ArgumentException("Money amount cannot be less than zero!");

            TotalMoneyAmountBeforeBeatingLevel = moneyAmount;
        }
        
        public static void SetTotalMoneyAmount(int totalMoneyAmount)
        {
            if (totalMoneyAmount < 0)
                throw new ArgumentException("Total money amount cannot be less than zero!");

            TotalMoneyAmount = totalMoneyAmount;
        }

        public static bool TryPurchaseWeapon(int weaponPrice)
        {
            if (weaponPrice > TotalMoneyAmount)
                return false;
        
            TotalMoneyAmount -= weaponPrice;

            return true;
        }
    }
}
