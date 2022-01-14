using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //eto yung tatawagin pag mag sasave
    public static void SavePlayer(PlayerBar player, Gold gold, SaveQuestScript updater, Inventory inventory, ClotheinInventory clothes)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        //instantiante the class of data holder of player ccreate tong script nato bukod sa script nato bale parang compilation of data ng player yung PlayerData para dun mo nalang kukunin pag isasave mo na
        PlayerData data = new PlayerData(player, gold,  updater, inventory, clothes);

        formatter.Serialize(stream, data);
        stream.Close();


    }

    //eto yung tatawagin pag mag loload
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.txt";

        //pag existing yung path na niloload mo
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //save sa variable nato yung mga data na na load
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

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
