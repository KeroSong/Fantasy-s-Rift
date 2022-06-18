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
            else
            {
                this.LOG("Vous ne pouvez pas vous équipez de cette objet car vous n'avez plus de place sur vos mecha pour ce type d'item.");
            }
        }
        else
        {
            this.LOG("Vous ne pouvez pas vous équipez de cette objet car ce n'est pas un objet équipable.");
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
        else
        {
            this.LOG("Vous ne pouvez pas vous mettre cette objet dans votre inventaire car il est plein.");
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
        int Price = transform.parent.GetComponent<ItemOnObject>().item.itemValue;
        PlayerPrefs.SetInt("GainGold", Price);
        m_InventoryPanel.GetComponent<InventoryItem>().GainGold();

        if (!(PlayerPrefs.GetInt("GainGold") == Price))
        {
            Text textId = transform.GetChild(0).GetComponent<Text>();
            int IDParent = transform.parent.parent.GetComponent<AttributeAId>().ID;
            m_InventoryPanel.GetComponent<InventoryItem>().RemoveItemToInventory(int.Parse(textId.text), IDParent);
        }
        else
        {
            this.LOG("Cet objet se vend trop cher, vous ne pouvez pas cumulez autant d'argent, dépensez un peu avant de vendre cet objet.");
        } 

        PlayerPrefs.SetInt("GainGold", 0);
    }
}