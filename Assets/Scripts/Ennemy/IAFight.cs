using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAFight : MonoBehaviour
{
    public float IADamage = 10f;

    [SerializeField] ProgressBar PbHealthMech1;
    [SerializeField] ProgressBar PbHealthMech2;

    Animator MonsterAnimator;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageToPlayer()
    {
        int x = Random.Range(0, 2);
        if(x== 0) {
            IADamage -= Armor1();
            PbHealthMech1.Val -= IADamage;
            if (PbHealthMech1.Val == 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().isDead();
            }
        }
        else
        {
            IADamage -= Armor2();
            PbHealthMech2.Val -= IADamage;

            if (PbHealthMech2.Val == 0)
            {

                GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().isDead();
            }
        }
    }
    public void isDead()
    {
        MonsterAnimator.SetTrigger("dead");
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
        foreach ( int Id in m_IdListe1) { 
            if (Id != 0 && i !=6)
            {
                Item item = inventoryItemList.itemList[Id];
                armor += item.itemAttributes[0].attributeValue;

            }
            i++;
        }
        return armor;
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
        return armor;
    }
}
