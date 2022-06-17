using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryItem : MonoBehaviour
{
    static ItemDataBaseList inventoryItemList;

    // Start is called before the first frame update
    void Update()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<ItemOnObject>().item = inventoryItemList.itemList[36];
        //this.LOG(inventoryItemList.itemList[36].itemName.ToString());
    }     
}
