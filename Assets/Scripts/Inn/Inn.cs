using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inn : MonoBehaviour
{
    Inventaire inventaire;

    public void SleepInsideAInn()
    {
        if(EnoughMoney())
        {
            if (PlayerPrefs.GetInt("classe") == 0)
            {
                PlayerPrefs.SetFloat("Santé", 20);
            }
            else if (PlayerPrefs.GetInt("classe") == 1)
            {
                PlayerPrefs.SetFloat("Santé", 15);
            }
            else
            {
                PlayerPrefs.SetFloat("Santé", 10);
            }

            PlayerPrefs.SetFloat("Santé_AI1", 20);
            PlayerPrefs.SetFloat("Santé_AI2", 15);
            PlayerPrefs.SetFloat("Santé_AI3", 10);
        }
    }

    bool EnoughMoney()
    {
        int Gold = ListeLoadGold();
        if (Gold < 10)
        {
            this.LOG("Vous n'avez pas assez d'argents pour aller à l'auberge");
            return false;
        }
        else
        {
            Gold = Gold - 10;
            PlayerPrefs.SetInt("OrTotal", Gold);
            ListeSaveGold();
            return true;
        }
    }

    int ListeLoadGold()
    {
        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        return inventaire.Gold;
    }

    void ListeSaveGold()
    {
        inventaire.Gold = PlayerPrefs.GetInt("OrTotal");

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = JsonUtility.ToJson(inventaire);
        System.IO.File.WriteAllText(filePath, data);
    }
}
