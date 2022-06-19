using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SDD.Events;

public class Save : MonoBehaviour
{
    public Scene scene = new Scene();
    public Inventaire inventaire = new Inventaire();

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
        scene.player.healthAI1 = PlayerPrefs.GetInt("Santé_AI1");
        scene.player.healthAI2 = PlayerPrefs.GetInt("Santé_AI2");
        scene.player.healthAI3 = PlayerPrefs.GetInt("Santé_AI3");

        scene.mecha1.head = PlayerPrefs.GetInt("Mecha1Tete");
        scene.mecha1.body = PlayerPrefs.GetInt("Mecha1Corps");
        scene.mecha2.head = PlayerPrefs.GetInt("Mecha2Tete");
        scene.mecha2.body = PlayerPrefs.GetInt("Mecha2Corps");

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
            PlayerPrefs.GetInt("Santé_AI1", scene.player.healthAI1);
            PlayerPrefs.GetInt("Santé_AI2", scene.player.healthAI2);
            PlayerPrefs.GetInt("Santé_AI3", scene.player.healthAI3);

            PlayerPrefs.SetInt("Mecha1Tete", scene.mecha1.head);
            PlayerPrefs.SetInt("Mecha1Corps", scene.mecha1.body);
            PlayerPrefs.SetInt("Mecha2Tete", scene.mecha2.head);
            PlayerPrefs.SetInt("Mecha2Corps", scene.mecha2.body);

            if (scene.player.classe == 0)
            {
                PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)0.9);
            }
            else if (scene.player.classe == 1)
            {
                PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)1);
            }
            else
            {
                PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)0.5);
            }

            EventManager.Instance.Raise(new PlayButtonClickedEvent());
        }
        else
        {
            this.LOG("Unity n'a pas trouvé de fichier de sauvegarde");
        }
    }
        
}

public class Scene
{
    public SavePlayer player = new SavePlayer();
    public Mecha mecha1 = new Mecha();
    public Mecha mecha2 = new Mecha();
}

public class SavePlayer
{
    public Vector3 position;
    public string nom;
    public int sexe;
    public int classe;
    public int health;
    public int healthAI1;
    public int healthAI2;
    public int healthAI3;
}

public class Mecha
{
    public int head;
    public int body;
}

public class Inventaire
{
    public int Gold;
    public List<int> items = new List<int>();
    public List<int> equip1 = new List<int>();
    public List<int> equip2 = new List<int>();
}

public class Items
{
    public int IdItem;
}

public class Dragon
{
    public List<bool> listDragon = new List<bool>();
}