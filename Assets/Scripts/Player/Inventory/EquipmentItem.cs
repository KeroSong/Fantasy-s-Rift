using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItem : MonoBehaviour
{
    static ItemDataBaseList inventoryItemList;
    [SerializeField] Sprite image;

    Inventaire inventaire;
    List<int> m_IdListe1;
    List<int> m_IdListe2;

    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe1 = inventaire.equip1;
        m_IdListe2 = inventaire.equip2;
    }

    void Update()
    {
        int i = 0;

        foreach (int Id in m_IdListe1)
        {
            if (Id == 0)
            {
                this.transform.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item.itemIcon = image;
            }
            else
            {
                this.transform.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[Id];
            }
            i++;
        }

        i = 0;

        foreach (int Id in m_IdListe2)
        {
            if (Id == 0)
            {
                this.transform.GetChild(2).GetChild(2).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item.itemIcon = image;
            }
            else
            {
                this.transform.GetChild(2).GetChild(2).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[Id];
            }
            i++;
        }
    } 
}