using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;

public class InventoryItem : MonoBehaviour
{

    static ItemDataBaseList inventoryItemList;

    Inventaire inventaire;
    List<int> m_IdListe;

    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
    }

    void Update()
    {
        m_IdListe = ListeLoad();

        if (PlayerPrefs.GetInt("IdItem") != 0)
        {
            AddItemToInventory();
        }

        Affichage(m_IdListe);

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

    void Affichage(List<int> m_IdListe)
    {
        int i = 0;

        foreach (int Id in m_IdListe)
        {
            if (Id == 0)
            {
                this.transform.GetChild(1).GetChild(i).GetChild(0).transform.localPosition = new Vector3(1000,1000,0);
            }
            else
            {
                this.transform.GetChild(1).GetChild(i).GetChild(0).transform.localPosition = Vector3.zero;
                this.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[Id];
            }
            i++;
        }
    }

    public void AddItemToInventory()
    {
        m_IdListe = ListeLoad();

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

        ListeSave(m_IdListe);
    }

    public void RemoveItemToInventory(int IdItem, int IdEmplacement)
    {
        m_IdListe = ListeLoad();
        if (m_IdListe[IdEmplacement] == IdItem)
        {
            m_IdListe[IdEmplacement] = 0;
        }

        ListeSave(m_IdListe);
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
        inventaire.Gold = PlayerPrefs.GetInt("OrTotal");
        
        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = JsonUtility.ToJson(inventaire);
        System.IO.File.WriteAllText(filePath, data);
    }

    void LostGold(int Gold)
    {
        m_IdListe = ListeLoad();

        if (Gold - PlayerPrefs.GetInt("LostGold") > 0)
        {
            Gold = Gold - PlayerPrefs.GetInt("LostGold");
            PlayerPrefs.SetInt("LostGold", 0);
            PlayerPrefs.SetInt("OrTotal", Gold);
        }

        inventaire.items = m_IdListe;
        inventaire.Gold = PlayerPrefs.GetInt("OrTotal");
        
        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = JsonUtility.ToJson(inventaire);
        System.IO.File.WriteAllText(filePath, data);
    }

    List<int> ListeLoad()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe = inventaire.items;
        return m_IdListe;
    }

    void ListeSave(List<int> m_IdListe)
    {
        inventaire.items = m_IdListe;

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = JsonUtility.ToJson(inventaire);
        System.IO.File.WriteAllText(filePath, data);
    }
}