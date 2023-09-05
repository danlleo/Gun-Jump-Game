using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoaderController
{
    private static string s_saveFilePath;

    public static void Save(SaveData passedSaveData)
    {
        s_saveFilePath = Application.persistentDataPath + "/gunJumpSave.json";

        SaveData saveData = new()
        {
            MoneyAmount = passedSaveData.MoneyAmount,
            CurrentLevel = passedSaveData.CurrentLevel,
            PurchasedWeaponPrefabIDList = passedSaveData.PurchasedWeaponPrefabIDList
        };

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(s_saveFilePath, json);

#if UNITY_EDITOR
        Debug.Log("Game Saved");
#endif
    }

    public static SaveData Load()
    {
        s_saveFilePath = Application.persistentDataPath + "/gunJumpSave.json";

        if (!SaveExists())
            return NewGame();
        
        string json = File.ReadAllText(s_saveFilePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

#if UNITY_EDITOR
        Debug.Log("Game Loaded");
#endif

        return saveData;
    }

    public static SaveData NewGame()
    {
        s_saveFilePath = Application.persistentDataPath + "/gunJumpSave.json";

        if (SaveExists()) 
            File.Delete(s_saveFilePath);

        SaveData saveData = new()
        {
            MoneyAmount = 0,
            CurrentLevel = 1,
            PurchasedWeaponPrefabIDList = new List<string>
            {
                // Give player only pistol as a default weapon
                "01f01438-4bf9-11ee-be56-0242ac120002",
            }
        };

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(s_saveFilePath, json);

#if UNITY_EDITOR
        Debug.Log("Game Saved");
#endif

        Save(saveData);

        return saveData;
    }

    private static bool SaveExists()
        => File.Exists(s_saveFilePath);

    // !!! REMOVE LATER !!!
    public static void DeleteSave()
    {
        File.Delete(s_saveFilePath);
        Debug.Log("Save deleted");
    }
}
