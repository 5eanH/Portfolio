using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private string saveFilePath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            saveFilePath = Application.persistentDataPath + "/saveData.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(int score)
    {
        SaveData data = new SaveData
        {
            score = score
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved: " + saveFilePath);
    }

    public int LoadScore()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Game Loaded: " + saveFilePath);
            return data.score;
        }
        else
        {
            Debug.Log("No save file found at: " + saveFilePath);
            return 0;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int score;
}