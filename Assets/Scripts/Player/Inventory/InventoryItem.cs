using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;

public class InventoryItem : MonoBehaviour
{
    static ItemDataBaseList inventoryItemList;
    [SerializeField] Sprite image;

    Inventaire inventaire;
    List<int> m_IdListe;

    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        string filePath = Application.persistentDataPath + "/All.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe = inventaire.items;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("IdItem") != 0)
        {
            AddItemToInventory();
        }

        int i = 0;

        foreach (int Id in m_IdListe)
        {
            if (Id == 0)
            {
                this.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item.itemIcon = image;
            }
            else
            {
                this.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[Id];
            }
            i++;
        }

        int Gold = PlayerPrefs.GetInt("OrTotal");

        if (PlayerPrefs.GetInt("GainGold") != 0)
        {
            GainGold(Gold);
        }

        if (PlayerPrefs.GetInt("LostGold") != 0)
        {
            LostGold(Gold);
        }
    } 

    void AddItemToInventory()
    {
        int i = 0;
        foreach (int Id in m_IdListe)
        {
            if (Id == 0)
            {
                m_IdListe[i] = PlayerPrefs.GetInt("IdItem");
                PlayerPrefs.SetInt("IdItem", 0);
                break;
            }
            i++;
        }

        inventaire.items = m_IdListe;

        string data = JsonUtility.ToJson(inventaire);
        string filePath = Application.persistentDataPath + "/All.json";
        System.IO.File.WriteAllText(filePath, data);
    }

    void GainGold(int Gold)
    {
        if (PlayerPrefs.GetInt("GainGold") < inventoryItemList.itemList[34].maxStack)
        {
            Gold = Gold + PlayerPrefs.GetInt("GainGold");
            PlayerPrefs.SetInt("GainGold", 0);
            PlayerPrefs.SetInt("OrTotal", Gold);
        }

        inventaire.items = m_IdListe;
        
        string data = JsonUtility.ToJson(inventaire);
        string filePath = Application.persistentDataPath + "/All.json";
        System.IO.File.WriteAllText(filePath, data);
    }

    void LostGold(int Gold)
    {
        if (Gold - PlayerPrefs.GetInt("LostGold") > 0)
        {
            Gold = Gold - PlayerPrefs.GetInt("LostGold");
            PlayerPrefs.SetInt("LostGold", 0);
            PlayerPrefs.SetInt("OrTotal", Gold);
        }

        inventaire.items = m_IdListe;
        
        string data = JsonUtility.ToJson(inventaire);
        string filePath = Application.persistentDataPath + "/All.json";
        System.IO.File.WriteAllText(filePath, data);
    }
}