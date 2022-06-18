using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemOnObject : MonoBehaviour                   //Saves the Item in the slot
{
    public Item item;                                       //Item 
    private Text textQuantity;                                      //text for the itemValue
    private Text textId;
    private Image image;

    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;                 //set the sprite of the Item 
        textQuantity = transform.GetChild(1).GetComponent<Text>();                                  //get the text(itemValue GameObject) of the item
        textId = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        if (GetComponent<ItemOnObject>().item.itemID == 34)
        {
            textQuantity.text = "" + item.itemValue;
        }
        else
        {
            textQuantity.text = "" + item.maxStack;
        }
        textId.text = "" + item.itemID;
        image.sprite = item.itemIcon;
    }
}
