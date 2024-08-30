using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static void SavePlayer(Click_Tracker ct)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        Player_Data data = new Player_Data(ct);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Player_Data LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Player_Data data = formatter.Deserialize(stream) as Player_Data;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveDino(Dinosaur d) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dino_name = d.GetName();
        string path = Application.persistentDataPath + "/" + dino_name + ".fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        Dino_Data data = new Dino_Data(d);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static Dino_Data LoadDino(Dinosaur d) {
        
        string dino_name = d.GetName();
        string path = Application.persistentDataPath + "/" + dino_name + ".fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Dino_Data data = formatter.Deserialize(stream) as Dino_Data;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
