using System.Collections.Generic;

[System.Serializable]
public class SaveData 
{
    public int MoneyAmount;
    public int CurrentLevel;
    public HashSet<string> PurchasedWeaponPrefabIDHashSet;
}
