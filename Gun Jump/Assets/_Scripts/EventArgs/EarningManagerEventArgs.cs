using System;

public class EarningManagerEventArgs : EventArgs
{
    public int CurrentMoneyAmount;

    public EarningManagerEventArgs(int currentMoneyAmount)
    {
        CurrentMoneyAmount = currentMoneyAmount;
    }
}
