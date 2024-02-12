
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PlayerData1
{
    public static void SaveGame(MenuScript manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();



    }

    public static PlayerData LoadGame()
    {
        string path = Application.persistentDataPath + "/player.fun";

        //UnComment this to delete the saved data
       // File.Delete(path);
        if(File.Exists(path)) 
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else 
        {
            Debug.LogError("Save File Not Found At:" + path);
            return null; 
        }



    }


}
