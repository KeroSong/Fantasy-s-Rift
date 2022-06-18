using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItem : MonoBehaviour
{
    static ItemDataBaseList inventoryItemList;

    Inventaire inventaire;
    List<int> m_IdListe1;
    List<int> m_IdListe2;

    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
    }

    void Update()
    {
        m_IdListe1 = Liste1Load();
        m_IdListe2 = Liste2Load();

        Affichage1(m_IdListe1);
        Affichage2(m_IdListe2);
    }

    void Affichage1(List<int> m_IdListe)
    {
        int i = 0;

        foreach (int Id in m_IdListe)
        {
            if (Id == 0)
            {
                this.transform.GetChild(1).GetChild(2).GetChild(i).GetChild(0).transform.localPosition = new Vector3(1000,1000,0);
            }
            else
            {
                this.transform.GetChild(1).GetChild(2).GetChild(i).GetChild(0).transform.localPosition = Vector3.zero;
                this.transform.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[Id];
            }
            i++;
        }
    }

    void Affichage2(List<int> m_IdListe)
    {
        int i = 0;

        foreach (int Id in m_IdListe)
        {
            if (Id == 0)
            {
                this.transform.GetChild(2).GetChild(2).GetChild(i).GetChild(0).transform.localPosition = new Vector3(1000,1000,0);
            }
            else
            {
                this.transform.GetChild(2).GetChild(2).GetChild(i).GetChild(0).transform.localPosition = Vector3.zero;
                this.transform.GetChild(2).GetChild(2).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[Id];
            }
            i++;
        }
    }

    public int AddEquipment(ItemType type, int Id)
    {
        m_IdListe1 = Liste1Load();
        m_IdListe2 = Liste2Load();

        switch (type)
        {
            case ItemType.Head:
                if (m_IdListe1[0] == 0)
                {
                    m_IdListe1[0] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[0] == 0)
                {
                    m_IdListe2[0] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            case ItemType.Necklace:
                if (m_IdListe1[1] == 0)
                {
                    m_IdListe1[1] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[1] == 0)
                {
                    m_IdListe2[1] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            case ItemType.Chest:
                if (m_IdListe1[2] == 0)
                {
                    m_IdListe1[2] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[2] == 0)
                {
                    m_IdListe2[2] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            case ItemType.Hands:
                if (m_IdListe1[3] == 0)
                {
                    m_IdListe1[3] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[3] == 0)
                {
                    m_IdListe2[3] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            case ItemType.Ring:
                if (m_IdListe1[4] == 0)
                {
                    m_IdListe1[4] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[4] == 0)
                {
                    m_IdListe2[4] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            case ItemType.Shoe:
                if (m_IdListe1[5] == 0)
                {
                    m_IdListe1[5] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[5] == 0)
                {
                    m_IdListe2[5] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            case ItemType.Weapon:
                if (m_IdListe1[6] == 0)
                {
                    m_IdListe1[6] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[6] == 0)
                {
                    m_IdListe2[6] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            case ItemType.Shield:
                if (m_IdListe1[7] == 0)
                {
                    m_IdListe1[7] = Id;

                    Liste1Save(m_IdListe1);
                    return 1;
                }
                else if (m_IdListe2[7] == 0)
                {
                    m_IdListe2[7] = Id;

                    Liste2Save(m_IdListe2);
                    return 1;
                }
                else
                {
                    return 0;
                }
                break;
            default:
                return 0;
                break;
        }
    }

    public void RemoveEquipment(ItemType type, int Id)
    {
        m_IdListe1 = Liste1Load();
        m_IdListe2 = Liste2Load();

        switch (type)
        {
            case ItemType.Head:
                if (Id == 0)
                {
                    m_IdListe1[0] = 0;
                }
                else
                {
                    m_IdListe2[0] = 0;
                }
                break;
            case ItemType.Necklace:
                if (Id == 0)
                {
                    m_IdListe1[1] = 0;
                }
                else
                {
                    m_IdListe2[1] = 0;
                }
                break;
            case ItemType.Chest:
                if (Id == 0)
                {
                    m_IdListe1[2] = 0;
                }
                else
                {
                    m_IdListe2[2] = 0;
                }
                break;
            case ItemType.Hands:
                if (Id == 0)
                {
                    m_IdListe1[3] = 0;
                }
                else
                {
                    m_IdListe2[3] = 0;
                }
                break;
            case ItemType.Ring:
                if (Id == 0)
                {
                    m_IdListe1[4] = 0;
                }
                else
                {
                    m_IdListe2[4] = 0;
                }
                break;
            case ItemType.Shoe:
                if (Id == 0)
                {
                    m_IdListe1[5] = 0;
                }
                else
                {
                    m_IdListe2[5] = 0;
                }
                break;
            case ItemType.Weapon:
                if (Id == 0)
                {
                    m_IdListe1[6] = 0;
                }
                else
                {
                    m_IdListe2[6] = 0;
                }
                break;
            case ItemType.Shield:
                if (Id == 0)
                {
                    m_IdListe1[7] = 0;
                }
                else
                {
                    m_IdListe2[7] = 0;
                }
                break;
            default:
                break;
        }

        Liste1Save(m_IdListe1);
        Liste2Save(m_IdListe2);
    }

    List<int> Liste1Load()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe1 = inventaire.equip1;
        return m_IdListe1;
    }

    List<int> Liste2Load()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe2 = inventaire.equip2;
        return m_IdListe2;
    }

    void Liste1Save(List<int> m_IdListe)
    {
        inventaire.equip1 = m_IdListe;

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = JsonUtility.ToJson(inventaire);
        System.IO.File.WriteAllText(filePath, data);
    }

    void Liste2Save(List<int> m_IdListe)
    {
        inventaire.equip2 = m_IdListe;

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = JsonUtility.ToJson(inventaire);
        System.IO.File.WriteAllText(filePath, data);
    }
}