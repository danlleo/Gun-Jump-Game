using System.Collections.Generic;

namespace _Scripts.Save
{
    [System.Serializable]
    public class SaveData 
    {
        public int MoneyAmount;
        public int CurrentLevel;
        public List<string> PurchasedWeaponPrefabIDList;
    }
}
