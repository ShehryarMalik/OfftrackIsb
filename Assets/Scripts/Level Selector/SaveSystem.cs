using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void saveGame(int clearedLevel)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelsData data = new LevelsData(clearedLevel);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelsData LoadLevel()
    {
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelsData data = formatter.Deserialize(stream) as LevelsData;
            stream.Close();

            return data;
        }

        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
