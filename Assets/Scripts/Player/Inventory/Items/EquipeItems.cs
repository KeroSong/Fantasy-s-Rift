using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipeItems : MonoBehaviour
{
    [SerializeField] GameObject m_InventoryPanel;
    [SerializeField] GameObject m_EquipmentPanel;
    [SerializeField] GameObject m_ShopPanel;

    public void EquipeItem()
    {
        ItemType type = transform.parent.GetComponent<ItemOnObject>().item.itemType;
        string typeText = type.ToString();

        if (typeText == "Weapon" || typeText == "Head" || typeText == "Shoe" || typeText == "Chest" || typeText == "Necklace" || typeText == "Ring" || typeText == "Hands" || typeText == "Shield")
        {
            Text textId = transform.GetChild(0).GetComponent<Text>();
            int libre = m_EquipmentPanel.GetComponent<EquipmentItem>().AddEquipment(type, int.Parse(textId.text));
            if (libre == 1)
            {
                int IDParent = transform.parent.parent.GetComponent<AttributeAId>().ID;
                m_InventoryPanel.GetComponent<InventoryItem>().RemoveItemToInventory(int.Parse(textId.text), IDParent);
            }
        }
    }

    public void UnequipeItem()
    {
        Text textId = transform.GetChild(0).GetComponent<Text>();
        PlayerPrefs.SetInt("IdItem", int.Parse(textId.text));

        m_InventoryPanel.GetComponent<InventoryItem>().AddItemToInventory();

        if (!(PlayerPrefs.GetInt("IdItem") == int.Parse(textId.text)))
        {
            ItemType type = transform.parent.GetComponent<ItemOnObject>().item.itemType;
            int IDParent = transform.parent.parent.GetComponent<AttributeAId>().ID;
            m_EquipmentPanel.GetComponent<EquipmentItem>().RemoveEquipment(type, IDParent);
        }

        PlayerPrefs.SetInt("IdItem", 0);
    }

    public void BuyItem()
    {
        /*Text textId = transform.GetChild(0).GetComponent<Text>();
        PlayerPrefs.SetInt("IdItem", int.Parse(textId.text));

        m_InventoryPanel.GetComponent<InventoryItem>().AddItemToInventory();

        if (!(PlayerPrefs.GetInt("IdItem") == int.Parse(textId.text)))
        {
            ItemType type = transform.parent.GetComponent<ItemOnObject>().item.itemType;
            int IDParent = transform.parent.parent.GetComponent<AttributeAId>().ID;
            m_EquipmentPanel.GetComponent<EquipmentItem>().RemoveEquipment(type, IDParent);
        }

        PlayerPrefs.SetInt("IdItem", 0);*/
    }

    public void SellItem()
    {
        /*ItemType type = transform.parent.GetComponent<ItemOnObject>().item.itemType;
        string typeText = type.ToString();

        if (typeText == "Weapon" || typeText == "Head" || typeText == "Shoe" || typeText == "Chest" || typeText == "Necklace" || typeText == "Ring" || typeText == "Hands" || typeText == "Shield")
        {
            Text textId = transform.GetChild(0).GetComponent<Text>();
            int libre = m_EquipmentPanel.GetComponent<EquipmentItem>().AddEquipment(type, int.Parse(textId.text));
            if (libre == 1)
            {
                int IDParent = transform.parent.parent.GetComponent<AttributeAId>().ID;
                m_InventoryPanel.GetComponent<InventoryItem>().RemoveItemToInventory(int.Parse(textId.text), IDParent);
            }
        }*/
    }
}