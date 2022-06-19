using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2mech : MonoBehaviour
{
    public int PlayerDamage = 10;

    [SerializeField] ProgressBar PbHealthMonster1;
    [SerializeField] ProgressBar PbHealthMonster2;
    [SerializeField] ProgressBar PbHealthMonster3;
    Animator MechAnimator;

    static ItemDataBaseList inventoryItemList;
    Inventaire inventaire;
    List<int> m_IdListe2;

    public bool dead = false;

    /*test*/
    private void Awake()
    {
        MechAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attack()
    {
        MechAnimator.SetTrigger("attack");

    }


    public void isDead()
    {
        MechAnimator.SetTrigger("dead");
        dead = true;
        GetComponent<player1mech>().enabled = false;
    }

    public void DamageToMonster1()
    {
        PlayerDamage += DamageWeapon();
        PbHealthMonster1.Val -= PlayerDamage;
        if (PbHealthMonster1.Val == 0)
        {
            GameObject.FindGameObjectWithTag("Monster1").GetComponent<IAFight>().isDead();
        }
    }
    public void DamageToMonster2()
    {
        PlayerDamage += DamageWeapon();
        PbHealthMonster2.Val -= PlayerDamage;
        if (PbHealthMonster2.Val == 0)
        {
            GameObject.FindGameObjectWithTag("Monster2").GetComponent<IAFight>().isDead();
        }
        
    }
    public void DamageToMonster3()
    {
        PlayerDamage += DamageWeapon();
        PbHealthMonster3.Val -= PlayerDamage;
        if (PbHealthMonster3.Val == 0)
        {
            GameObject.FindGameObjectWithTag("Monster3").GetComponent<IAFight>().isDead();
        }
    }


    List<int> Liste2Load()
    {
        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe2 = inventaire.equip2;
        return m_IdListe2;
    }

    int DamageWeapon()
    {
        m_IdListe2 = Liste2Load();
        int Id = m_IdListe2[6];
        if (Id == 0)
        {
            return 0;
        }
        else
        {
            Item item = inventoryItemList.itemList[Id];
            int damage = item.itemAttributes[0].attributeValue;
            return damage;
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
    }
}
