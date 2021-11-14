using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(PlayerController player)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/player.save";
        var stream = new FileStream(path, FileMode.Create);
        
        var data = new SavedData(player);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SavedData LoadGame()
    {
        var path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            var data = formatter.Deserialize(stream) as SavedData;
            stream.Close();
            
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}