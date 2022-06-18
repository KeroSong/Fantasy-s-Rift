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
        Text textId = transform.GetChild(0).GetComponent<Text>();

        if (int.Parse(textId.text) != 34)
        {
            ItemType type = transform.parent.GetComponent<ItemOnObject>().item.itemType;
            string typeText = type.ToString();

            if (typeText == "Weapon" || typeText == "Head" || typeText == "Shoe" || typeText == "Chest" || typeText == "Necklace" || typeText == "Ring" || typeText == "Hands" || typeText == "Shield")
            {
                int libre = m_EquipmentPanel.GetComponent<EquipmentItem>().AddEquipment(type, int.Parse(textId.text));
                if (libre == 1)
                {
                    int IDParent = transform.parent.parent.GetComponent<AttributeAId>().ID;
                    m_InventoryPanel.GetComponent<InventoryItem>().RemoveItemToInventory(int.Parse(textId.text), IDParent);
                }
                else
                {
                    this.LOG("Vous ne pouvez pas vous équipez de cet objet car vous n'avez plus de place sur vos mecha pour ce type d'objet.");
                }
            }
            else
            {
                this.LOG("Vous ne pouvez pas vous équipez de cet objet car ce n'est pas un objet équipable.");
            }
        }
        else
        {
            this.LOG("Vous ne pouvez pas vous équipez avec de l'or.");
        }
    }

    public void UnequipeItem()
    {
        Text textId = transform.GetChild(0).GetComponent<Text>();

        if (int.Parse(textId.text) != 34)
        {
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
                this.LOG("Vous ne pouvez pas mettre cet objet dans votre inventaire car il est plein.");
            }
            PlayerPrefs.SetInt("IdItem", 0);
        }
        else
        {
            this.LOG("Comment vous avez fait pour vous équipez avec de l'or ? Pour la peine, vous ne pourrez pas le retirer.");
        }
    }

    public void BuyItem()
    {
        Text textId = transform.GetChild(0).GetComponent<Text>();

        if (int.Parse(textId.text) != 34)
        {
            int Price = transform.parent.GetComponent<ItemOnObjectShop>().item.itemValue;
            PlayerPrefs.SetInt("LostGold", Price);
            m_InventoryPanel.GetComponent<InventoryShopItem>().LostGold();

            if (!(PlayerPrefs.GetInt("LostGold") == Price))
            {
                PlayerPrefs.SetInt("IdItem", int.Parse(textId.text));
                m_InventoryPanel.GetComponent<InventoryShopItem>().AddItemToInventory();

                if (PlayerPrefs.GetInt("IdItem") == int.Parse(textId.text))
                {
                    this.LOG("Cet objet ne peut pas être acheté car vous n'avez plus de place dans votre inventaire, vendez des objets pour faire de la place.");
                }
            }
            else
            {
                this.LOG("Cet objet ne peut pas être acheté car il est trop cher pour vous, vendez des objets ou faites des combat pour gagner plus d'argent.");
            }

            PlayerPrefs.SetInt("IdItem", 0);
            PlayerPrefs.SetInt("LostGold", 0);
        }
        else
        {
            this.LOG("Il n'y a pas d'or dans le magasin mais au cas où...");
        }
    }

    public void SellItem()
    {
        Text textId = transform.GetChild(0).GetComponent<Text>();

        if (int.Parse(textId.text) != 34)
        {
            int Price = transform.parent.GetComponent<ItemOnObjectShop>().item.itemValue;
            PlayerPrefs.SetInt("GainGold", Price);
            m_InventoryPanel.GetComponent<InventoryShopItem>().GainGold();

            if (!(PlayerPrefs.GetInt("GainGold") == Price))
            {
                int IDParent = transform.parent.parent.GetComponent<AttributeAId>().ID;
                m_InventoryPanel.GetComponent<InventoryShopItem>().RemoveItemToInventory(int.Parse(textId.text), IDParent);
            }
            else
            {
                this.LOG("Cet objet coûte cher, vous ne pouvez pas cumulez plus d'argent, dépensez un peu avant de vendre cet objet.");
            }

            PlayerPrefs.SetInt("GainGold", 0);
        }
        else
        {
            this.LOG("Vous ne pouvez pas vendre de l'or.");
        }
    }
}