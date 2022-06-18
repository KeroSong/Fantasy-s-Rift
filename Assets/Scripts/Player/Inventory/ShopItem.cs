using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;

public class ShopItem : MonoBehaviour
{

    static ItemDataBaseList inventoryItemList;

    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
    }

    void Update()
    {
        Affichage();
    }

    void Affichage()
    {

        for (int i = 0; i < 33; i++)
        {
            this.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObjectShop>().item = inventoryItemList.itemList[i+1];
        }
    }
}