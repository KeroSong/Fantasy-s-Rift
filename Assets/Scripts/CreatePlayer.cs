using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour
{
    [SerializeField] Image m_WarriorImage;
    [SerializeField] Sprite m_WarriorWoman;
    [SerializeField] Image m_ArcherImage;
    [SerializeField] Sprite m_ArcherWoman;
    [SerializeField] Image m_WizardImage;
    [SerializeField] Sprite m_WizardWoman;

    void Start()
    {
        SetText();
    }

    void SetText()
    {
        PlayerPrefs.SetString("Nom", "Henry");
        PlayerPrefs.SetInt("sexe", 0);
        PlayerPrefs.SetInt("classe", 0);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
        //PlayerPrefs.SetFloat("Santé", 50.0F);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("sexe") == 1)
        {
            m_WarriorImage.sprite = m_WarriorWoman;
            m_ArcherImage.sprite = m_ArcherWoman;
            m_WizardImage.sprite = m_WizardWoman;
        }
    }

    public void WomanHasBeenClicked()
    {
        PlayerPrefs.SetInt("sexe", 1);
    }

    public void ArcherHasBeenClicked()
    {
        PlayerPrefs.SetInt("classe", 1);
    }

    public void WizardHasBeenClicked()
    {
        PlayerPrefs.SetInt("classe", 2);
    }
}