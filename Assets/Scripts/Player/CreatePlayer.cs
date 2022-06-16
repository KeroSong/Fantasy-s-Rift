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
        PlayerPrefs.SetInt("Difficulté", 1);
        SetPlayer();
        SetAI1();
        SetAI2();
        SetAI3();
    }

    void SetPlayer()
    {
        PlayerPrefs.SetInt("Santé", 20);
        PlayerPrefs.SetInt("Mana", 10);
        PlayerPrefs.SetString("Nom", "Henry");
        PlayerPrefs.SetInt("sexe", 0);
        PlayerPrefs.SetInt("classe", 0);
        PlayerPrefs.SetInt("Niveau", 0);
        PlayerPrefs.SetInt("Expérience", 0);
        PlayerPrefs.SetInt("Gain", 0);
        PlayerPrefs.SetFloat("PositionX", (float)0.2);
        PlayerPrefs.SetFloat("PositionY", (float)19.9);
        PlayerPrefs.SetFloat("PositionZ", (float)10.4);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
    }

    void SetAI1()
    {
        PlayerPrefs.SetInt("Santé_AI1", 20);
        PlayerPrefs.SetInt("Mana_AI1", 10);
        PlayerPrefs.SetString("Nom_AI1", "Henry");
        PlayerPrefs.SetInt("sexe_AI1", 0);
        PlayerPrefs.SetInt("classe_AI1", 0);
        PlayerPrefs.SetInt("Niveau_AI1", 0);
        PlayerPrefs.SetInt("Expérience_AI1", 0);
        PlayerPrefs.SetInt("Gain_AI1", 0);
    }

    void SetAI2()
    {
        PlayerPrefs.SetInt("Santé_AI2", 15);
        PlayerPrefs.SetInt("Mana_AI2", 15);
        PlayerPrefs.SetString("Nom_AI2", "Henry");
        PlayerPrefs.SetInt("sexe_AI2", 1);
        PlayerPrefs.SetInt("classe_AI2", 1);
        PlayerPrefs.SetInt("Niveau_AI2", 0);
        PlayerPrefs.SetInt("Expérience_AI2", 0);
        PlayerPrefs.SetInt("Gain_AI2", 0);
    }

    void SetAI3()
    {
        PlayerPrefs.SetInt("Santé_AI3", 10);
        PlayerPrefs.SetInt("Mana_AI3", 20);
        PlayerPrefs.SetString("Nom_AI3", "Henry");
        PlayerPrefs.SetInt("sexe_AI3", 1);
        PlayerPrefs.SetInt("classe_AI3", 2);
        PlayerPrefs.SetInt("Niveau_AI3", 0);
        PlayerPrefs.SetInt("Expérience_AI3", 0);
        PlayerPrefs.SetInt("Gain_AI3", 0);
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
        PlayerPrefs.SetInt("Mana", 15);
    }

    public void WizardHasBeenClicked()
    {
        PlayerPrefs.SetInt("classe", 2);
        PlayerPrefs.SetInt("Santé", 10);
        PlayerPrefs.SetInt("Mana", 20);
    }
}