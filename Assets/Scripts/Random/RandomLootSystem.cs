using UnityEngine;
using System.Collections;

public class RandomLootSystem : MonoBehaviour
{
    static ItemDataBaseList inventoryItemList;

    // Use this for initialization
    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
        int count = inventoryItemList.itemList.Count;

        int Gold = Random.Range(20, 50);
        int RandomId = Random.Range(1, count - 2);

        this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[count - 1];
        this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<ItemOnObject>().item.itemValue = Gold;
        this.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[RandomId];

        PlayerPrefs.SetInt("GainGold", Gold);
        PlayerPrefs.SetInt("IdItem", RandomId);
    }
}