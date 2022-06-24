using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalDataManager
{
    
    private string filePath;

    // Start is called before the first frame update
    public LocalDataManager(string fileName)
    {
        filePath = Application.persistentDataPath + "/" + fileName;
        if (!File.Exists(filePath))
        {
            var data = new LocalSaveData();
            File.WriteAllText(filePath, JsonUtility.ToJson(data));
        }
    }


    public void WriteDataToFile(LocalSaveData data)
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(data));
    }


    public LocalSaveData ReadDataFromFile()
    {
        if (!File.Exists(filePath)) return null;
        return JsonUtility.FromJson<LocalSaveData>(File.ReadAllText(filePath));
    }
}
