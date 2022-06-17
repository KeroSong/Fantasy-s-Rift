using UnityEngine;
using System.Collections;

public class RandomLootSystem : MonoBehaviour
{
    static ItemDataBaseList inventoryItemList;

    // Use this for initialization
    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        int Gold = Random.Range(20, 50);
        int RandomId = Random.Range(1, inventoryItemList.itemList.Count - 2);

        this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[36];
        this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<ItemOnObject>().item.itemValue = Gold;
        this.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[RandomId];
    }
}