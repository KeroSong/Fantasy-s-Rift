using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour
{
    [SerializeField] Image m_WarriorButton;
    [SerializeField] Sprite m_WarriorWoman;

    string i;

    public SavePlayer player = new SavePlayer();

    /*public void ManHasBeenClicked()
    {
        player.sexe = false;
    }*/

    public void WomanHasBeenClicked()
    {
        player.sexe = true;
        //m_WarriorButton.sprite = m_WarriorWoman;
        this.LOG(player.sexe.ToString());
    }

    void Update()
    {
        this.LOG(player.sexe.ToString());
    }
}

[System.Serializable]
public class SavePlayer
{
    public Vector3 position;
    public string nom;
    public bool sexe;
    public int classe;
    public Inventory inventory = new Inventory();
}