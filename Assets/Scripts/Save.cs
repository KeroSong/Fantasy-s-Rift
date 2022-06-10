using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class Save : MonoBehaviour
{
    //SaveImage();
    public Scene scene = new Scene();

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<SaveButtonClickedEvent>(SaveButtonClicked);
        EventManager.Instance.AddListener<LoadButtonClickedEvent>(LoadButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<SaveButtonClickedEvent>(SaveButtonClicked);
        EventManager.Instance.RemoveListener<LoadButtonClickedEvent>(LoadButtonClicked);
    }

    private void OnEnable()
    {
        SubscribeEvents();    
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void SaveButtonClicked(SaveButtonClickedEvent e)
    {
        string data = JsonUtility.ToJson(scene);
        string filePath = Application.persistentDataPath + "/SaveData.json";
        System.IO.File.WriteAllText(filePath, data);
        this.LOG("Sauvegarde effectuée");
    }

    public void LoadButtonClicked(LoadButtonClickedEvent e)
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
    public List<Chest> chest = new List<Chest>();
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