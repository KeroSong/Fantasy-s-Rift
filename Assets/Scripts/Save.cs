using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SDD.Events;

public class Save : MonoBehaviour
{
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
        Vector3 position = new Vector3(
            PlayerPrefs.GetFloat("PositionX"),
            PlayerPrefs.GetFloat("PositionY"),
            PlayerPrefs.GetFloat("PositionZ"));
        scene.player.position = position;
        scene.player.nom = PlayerPrefs.GetString("Nom");
        scene.player.sexe = PlayerPrefs.GetInt("sexe");
        scene.player.classe = PlayerPrefs.GetInt("classe");
        scene.player.health = PlayerPrefs.GetInt("Santé");
        scene.player.mana = PlayerPrefs.GetInt("Mana");
        scene.player.level = PlayerPrefs.GetInt("Niveau");
        scene.player.experience = PlayerPrefs.GetInt("Expérience");

        string data = JsonUtility.ToJson(scene);
        string filePath = Application.persistentDataPath + "/SaveData.json";
        System.IO.File.WriteAllText(filePath, data);
        this.LOG("Sauvegarde effectuée");
    }

    public void LoadButtonClicked(LoadButtonClickedEvent e)
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(filePath))
        {
            string data = System.IO.File.ReadAllText(filePath);
            scene = JsonUtility.FromJson<Scene>(data);
            this.LOG("Chargement effectué");

            PlayerPrefs.SetFloat("PositionX", (float)scene.player.position.x);
            PlayerPrefs.SetFloat("PositionY", (float)scene.player.position.y);
            PlayerPrefs.SetFloat("PositionZ", (float)scene.player.position.z);
            PlayerPrefs.SetString("Nom", scene.player.nom);
            PlayerPrefs.SetInt("sexe", scene.player.sexe);
            PlayerPrefs.SetInt("classe", scene.player.classe);
            PlayerPrefs.SetInt("Santé", scene.player.health);
            PlayerPrefs.SetInt("Mana", scene.player.mana);
            PlayerPrefs.SetInt("Niveau", scene.player.level);
            PlayerPrefs.SetInt("Expérience", scene.player.experience);

            EventManager.Instance.Raise(new PlayButtonClickedEvent());
        }
        else
        {
            this.LOG("Unity n'a pas trouvé de fichier de sauvegarde");
        }
    }
        
}

[System.Serializable]
public class Scene
{
    public List<Chest> chest = new List<Chest>();
    public SavePlayer player = new SavePlayer();
    public Inventory inventory = new Inventory();
}

[System.Serializable]
public class SavePlayer
{
    public Vector3 position;
    public string nom;
    public int sexe;
    public int classe;
    public int health;
    public int mana;
    public int level;
    public int experience;
}

[System.Serializable]
public class Inventory
{
    public int goldCoins;
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