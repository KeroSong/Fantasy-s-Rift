using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour
{
    [SerializeField] Image m_WarriorImage;
    [SerializeField] Sprite m_WarriorMan;
    [SerializeField] Sprite m_WarriorWoman;
    [SerializeField] Image m_ArcherImage;
    [SerializeField] Sprite m_ArcherMan;
    [SerializeField] Sprite m_ArcherWoman;
    [SerializeField] Image m_WizardImage;
    [SerializeField] Sprite m_WizardMan;
    [SerializeField] Sprite m_WizardWoman;

    Inventaire m_All;
    List<int> m_IdInventory;
    List<int> m_IdEquipment1;
    List<int> m_IdEquipment2;

    void Start()
    {
        SetPlayer();
        SetAI1();
        SetAI2();
        SetAI3();
        Mecha();


        m_IdInventory.Add(34);
        for (int i = 1; i < 25; i++)
        {
            m_IdInventory.Add(0);
        }

        for (int i = 0; i < 8; i++)
        {
            m_IdEquipment1.Add(0);
            m_IdEquipment2.Add(0);
        }

        m_All.items = m_IdInventory;
        m_All.equip1 = m_IdInventory;
        m_All.equip2 = m_IdInventory;

        string data = JsonUtility.ToJson(m_All);
        string filePath = Application.persistentDataPath + "/All.json";
        System.IO.File.WriteAllText(filePath, data);
    }

    void SetPlayer()
    {
        PlayerPrefs.SetFloat("PositionX", (float)0.37);
        PlayerPrefs.SetFloat("PositionY", (float)11.93);
        PlayerPrefs.SetFloat("PositionZ", (float)10.4);
        PlayerPrefs.SetInt("Santé", 20);
        PlayerPrefs.SetString("Nom", "Henry");
        PlayerPrefs.SetInt("sexe", 0);
        PlayerPrefs.SetInt("classe", 0);
        PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)0.9);
        PlayerPrefs.SetInt("GainGold", 0);
        PlayerPrefs.SetInt("LostGold", 0);
        PlayerPrefs.SetInt("IdItem", 0);
        PlayerPrefs.SetInt("OrTotal", 0);
    }

    void SetAI1()
    {
        PlayerPrefs.SetInt("Santé_AI1", 20);
        PlayerPrefs.SetString("Nom_AI1", "Henry");
        PlayerPrefs.SetInt("sexe_AI1", 0);
        PlayerPrefs.SetInt("classe_AI1", 0);
        PlayerPrefs.SetFloat("VitesseJaugeAI1", (float)0.9);
    }

    void SetAI2()
    {
        PlayerPrefs.SetInt("Santé_AI2", 15);
        PlayerPrefs.SetString("Nom_AI2", "Henry");
        PlayerPrefs.SetInt("sexe_AI2", 1);
        PlayerPrefs.SetInt("classe_AI2", 1);
        PlayerPrefs.SetFloat("VitesseJaugeAI2", (float)1);
    }

    void SetAI3()
    {
        PlayerPrefs.SetInt("Santé_AI3", 10);
        PlayerPrefs.SetString("Nom_AI3", "Henry");
        PlayerPrefs.SetInt("sexe_AI3", 1);
        PlayerPrefs.SetInt("classe_AI3", 2);
        PlayerPrefs.SetFloat("VitesseJaugeAI3", (float)0.5);
    }

    void Mecha()
    {
        PlayerPrefs.SetFloat("JaugeMecha1", 0);
        PlayerPrefs.SetFloat("JaugeMecha2", 0);
        PlayerPrefs.SetInt("Mecha1Tete", 0);
        PlayerPrefs.SetInt("Mecha1Corps", 1);
        PlayerPrefs.SetInt("Mecha2Tete", 2);
        PlayerPrefs.SetInt("Mecha2Corps", 3);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("sexe") == 0)
        {
            m_WarriorImage.sprite = m_WarriorMan;
            m_ArcherImage.sprite = m_ArcherMan;
            m_WizardImage.sprite = m_WizardMan;
        }
        else if (PlayerPrefs.GetInt("sexe") == 1)
        {
            m_WarriorImage.sprite = m_WarriorWoman;
            m_ArcherImage.sprite = m_ArcherWoman;
            m_WizardImage.sprite = m_WizardWoman;
        }
    }

    public void ManHasBeenClicked()
    {
        PlayerPrefs.SetInt("sexe", 0);
    }

    public void WomanHasBeenClicked()
    {
        PlayerPrefs.SetInt("sexe", 1);
    }

    public void ArcherHasBeenClicked()
    {
        PlayerPrefs.SetInt("classe", 1);
        PlayerPrefs.SetInt("Santé", 15);
        PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)1);
    }

    public void WizardHasBeenClicked()
    {
        PlayerPrefs.SetInt("classe", 2);
        PlayerPrefs.SetInt("Santé", 10);
        PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)0.5);
    }
}