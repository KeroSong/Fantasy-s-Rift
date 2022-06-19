using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        SetPlayer();
        SetAI1();
        SetAI2();
        SetAI3();
        Mecha();
    }

    void SetPlayer()
    {
        PlayerPrefs.SetFloat("PositionX", (float)0.37);
        PlayerPrefs.SetFloat("PositionY", (float)11.93);
        PlayerPrefs.SetFloat("PositionZ", (float)10.4);
        PlayerPrefs.SetFloat("Santé", 20);
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
        PlayerPrefs.SetFloat("Santé_AI1", 20);
        PlayerPrefs.SetString("Nom_AI1", "Henry");
        PlayerPrefs.SetInt("sexe_AI1", 0);
        PlayerPrefs.SetInt("classe_AI1", 0);
        PlayerPrefs.SetFloat("VitesseJaugeAI1", (float)0.9);
    }

    void SetAI2()
    {
        PlayerPrefs.SetFloat("Santé_AI2", 15);
        PlayerPrefs.SetString("Nom_AI2", "Henry");
        PlayerPrefs.SetInt("sexe_AI2", 1);
        PlayerPrefs.SetInt("classe_AI2", 1);
        PlayerPrefs.SetFloat("VitesseJaugeAI2", (float)0.98);
    }

    void SetAI3()
    {
        PlayerPrefs.SetFloat("Santé_AI3", 10);
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
        PlayerPrefs.SetFloat("Santé", 15);
        PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)1);
    }

    public void WizardHasBeenClicked()
    {
        PlayerPrefs.SetInt("classe", 2);
        PlayerPrefs.SetFloat("Santé", 10);
        PlayerPrefs.SetFloat("VitesseJaugePlayer", (float)0.65);
    }
}