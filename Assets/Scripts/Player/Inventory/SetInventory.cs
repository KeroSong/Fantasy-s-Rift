using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetInventory : MonoBehaviour
{
    Inventaire m_All = new Inventaire();

    // Start is called before the first frame update
    void Start()
    {
        m_All.items.Add(34);
        for (int i = 1; i < 25; i++)
        {
            m_All.items.Add(0);
        }

        for (int i = 0; i < 8; i++)
        {
            m_All.equip1.Add(0);
            m_All.equip2.Add(0);
        }

        m_All.Gold = PlayerPrefs.GetInt("OrTotal");

        string data = JsonUtility.ToJson(m_All);
        string filePath = Application.persistentDataPath + "/AllInventory.json";
        System.IO.File.WriteAllText(filePath, data);
    }
}
