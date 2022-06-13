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
        SetText();
    }

    void SetText()
    {
        PlayerPrefs.SetInt("Santé", 20);
        PlayerPrefs.SetString("Nom", "Henry");
        PlayerPrefs.SetInt("sexe", 0);
        PlayerPrefs.SetInt("classe", 0);
        PlayerPrefs.SetInt("Niveau", 0);
        PlayerPrefs.SetInt("Expérience", 0);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
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
    }

    public void WizardHasBeenClicked()
    {
        PlayerPrefs.SetInt("classe", 2);
        PlayerPrefs.SetInt("Santé", 10);
    }
}