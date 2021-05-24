using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDataManager : MonoBehaviour
{
    [System.Serializable]
    private class SaveData
    {
        public List<StageData> stageDatas;
    }

    public static StageDataManager instance;

    public Dictionary<string, bool> stageDatas = new Dictionary<string, bool>();
    private readonly string saveFileName = "StageData";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Load();
    }

    public void Clear()
    {
        stageDatas[SceneManager.GetActiveScene().name] = true;

        Save();
    }

    private void Save()
    {
        SaveData saveData = new SaveData();
        saveData.stageDatas = new List<StageData>();

        foreach (string key in stageDatas.Keys)
        {
            StageData stageData = new StageData(key, stageDatas[key]);
            saveData.stageDatas.Add(stageData);
        }

        JsonIO.SaveToJson(saveData, saveFileName);
    }

    private void Load()
    {
        SaveData sd = JsonIO.LoadFromJson<SaveData>(saveFileName);

        if (sd == null) return;

        for (int i = 0; i < sd.stageDatas.Count; i++)
        {
            stageDatas.Add(sd.stageDatas[i].sceneName, sd.stageDatas[i].isClear);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[System.Serializable]
public class StageData
{
    public string sceneName;
    public bool isClear;

    public StageData(string sceneName, bool isClear)
    {
        this.sceneName = sceneName;
        this.isClear = isClear;
    }
}
