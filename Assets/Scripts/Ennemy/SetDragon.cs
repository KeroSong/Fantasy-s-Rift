using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetDragon : MonoBehaviour
{
    Dragon m_List = new Dragon();

    // Start is called before the first frame update
    void Start()
    {
        m_List.listDragon = new List<bool>() {false, false, false, false};

        string data = JsonUtility.ToJson(m_List);
        string filePath = Application.persistentDataPath + "/Dragon.json";
        System.IO.File.WriteAllText(filePath, data);
    }
}
