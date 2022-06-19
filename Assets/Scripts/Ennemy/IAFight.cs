using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAFight : MonoBehaviour
{
    public float IADamage = 10f;

    [SerializeField] ProgressBar PbHealthMech1;
    [SerializeField] ProgressBar PbHealthMech2;

    Animator MonsterAnimator;

    public bool dead = false;

    static ItemDataBaseList inventoryItemList;
    Inventaire inventaire;
    List<int> m_IdListe1;
    List<int> m_IdListe2;


    private void Awake()
    {
        MonsterAnimator = GetComponent<Animator>();
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

    public void DamageToPlayer()
    {
        int x = Random.Range(0, 2);
        if(x == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().dead == false)
        {
            IADamage -= Armor1();
            PbHealthMech1.Val -= IADamage;
            if (IADamage <= PlayerPrefs.GetFloat("Santé"))
            {
                PlayerPrefs.SetFloat("Santé", PlayerPrefs.GetFloat("Santé") - IADamage);
            }
            else
            {
                PlayerPrefs.SetFloat("Santé", 0);
                PlayerPrefs.SetFloat("Santé_AI1", PlayerPrefs.GetFloat("Santé_AI1") - IADamage);
            }

            if (PbHealthMech1.Val == 0)
            {
                IADamage -= PlayerPrefs.GetFloat("Santé");
                PlayerPrefs.SetFloat("Santé_AI1", 0);
                GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().isDead();
            }
        }
        else if(GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().dead == false)
        {
            IADamage -= Armor2();
            PbHealthMech2.Val -= IADamage;
            if (IADamage <= PlayerPrefs.GetFloat("Santé_AI2"))
            {
                PlayerPrefs.SetFloat("Santé_AI2", PlayerPrefs.GetFloat("Santé_AI2") - IADamage);
            }
            else
            {
                IADamage -= PlayerPrefs.GetFloat("Santé_AI2");
                PlayerPrefs.SetFloat("Santé_AI2", 0);
                PlayerPrefs.SetFloat("Santé_AI3", PlayerPrefs.GetFloat("Santé_AI3") - IADamage);
            }

            if (PbHealthMech2.Val == 0)
            {
                PlayerPrefs.SetFloat("Santé_AI3", 0);
                GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().isDead();
            }
        }
        else
        {
            IADamage -= Armor1();
            PbHealthMech1.Val -= IADamage;
            if (IADamage <= PlayerPrefs.GetFloat("Santé"))
            {
                PlayerPrefs.SetFloat("Santé", PlayerPrefs.GetFloat("Santé") - IADamage);
            }
            else
            {
                IADamage -= PlayerPrefs.GetFloat("Santé");
                PlayerPrefs.SetFloat("Santé", 0);
                PlayerPrefs.SetFloat("Santé_AI1", PlayerPrefs.GetFloat("Santé_AI1") - IADamage);
            }

            if (PbHealthMech1.Val == 0)
            {
                PlayerPrefs.SetFloat("Santé_AI1", 0);
                GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().isDead();
            }
        }
    }

    public void attack()
    {
        MonsterAnimator.SetTrigger("attack");
    }

    public void isDead()
    {
        MonsterAnimator.SetTrigger("dead");
        dead = true;
        GetComponent<IAFight>().enabled = false;
    }

    List<int> Liste1Load()
    {
        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe1 = inventaire.equip1;
        return m_IdListe1;
    }

    List<int> Liste2Load()
    {
        string filePath = Application.persistentDataPath + "/AllInventory.json";
        string data = System.IO.File.ReadAllText(filePath);
        inventaire = JsonUtility.FromJson<Inventaire>(data);

        m_IdListe2 = inventaire.equip2;
        return m_IdListe2;
    }

    int Armor1()
    {
        m_IdListe1 = Liste1Load();
        int armor = 0;
        int i = 0;
        foreach ( int Id in m_IdListe1)
        { 
            if (Id != 0 && i !=6)
            {
                Item item = inventoryItemList.itemList[Id];
                armor += item.itemAttributes[0].attributeValue;
            }
            i++;
        }
        return armor/10;
    }

    int Armor2()
    {
        m_IdListe2 = Liste2Load();
        int armor = 0;
        int i = 0;
        foreach (int Id in m_IdListe2)
        {
            if (Id != 0 && i != 6)
            {
                Item item = inventoryItemList.itemList[Id];
                armor += item.itemAttributes[0].attributeValue;

            }
            i++;
        }
        return armor/10;
    }
}
