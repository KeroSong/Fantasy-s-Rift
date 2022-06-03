using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    //SaveImage();
    public Scene scene = new Scene();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveToJSON();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadFromJSON();
        }
    }

    public void SaveToJSON()
    {
        string data = JsonUtility.ToJson(scene);
        string filePath = Application.persistentDataPath + "/SaveData.json";
        System.IO.File.WriteAllText(filePath, data);
        this.LOG("Sauvegarde effectuée");
    }

    public void LoadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string data = System.IO.File.ReadAllText(filePath);
        scene = JsonUtility.FromJson<Scene>(data);
        this.LOG("Chargement effectué");
    }
}

[System.Serializable]
public class Scene
{
    public Chest chest = new Chest();
    public SavePlayer player = new SavePlayer();
}

[System.Serializable]
public class SavePlayer
{
    public Vector3 position;
    public Inventory inventory = new Inventory();
}

[System.Serializable]
public class Inventory
{
    public int goldCoins;
    public bool isFull;
    public List<Items> items = new List<Items>();
}

[System.Serializable]
public class Items
{
    public string name;
    //public string desc;
}

[System.Serializable]
public class Chest
{
    public bool OpenClose;
}

/*void SaveImage()
{
    ScreenCapture.CaptureScreenshot("SomeLevel");
}*/