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

        /*float x = Random.Range(5, terrain.terrainData.size.x - 5);
        float z = Random.Range(5, terrain.terrainData.size.z - 5);


        if (inventoryItemList.itemList[randomNumber].itemModel == null)
        else
        {
            GameObject randomLootItem = (GameObject)Instantiate(inventoryItemList.itemList[randomNumber].itemModel);
            PickUpItem item = randomLootItem.AddComponent<PickUpItem>();
            item.item = inventoryItemList.itemList[randomNumber];

            randomLootItem.transform.localPosition = new Vector3(x, 0, z);
        }*/
    }
}
