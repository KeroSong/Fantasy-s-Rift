using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomEnemySystem : MonoBehaviour
{
    [SerializeField] int m_AmountOfEnemy = 5;

    [SerializeField] GameObject m_Goblin1;
    [SerializeField] GameObject m_Goblin2;
    [SerializeField] GameObject m_Goblin3;
    [SerializeField] GameObject m_Goblin4;
    [SerializeField] GameObject m_Goblin5;

    List<GameObject> m_Goblins;

    int counter = 0;

    private void Awake()
    {
        m_Goblins = new List<GameObject>() { m_Goblin1, m_Goblin2, m_Goblin3, m_Goblin4, m_Goblin5 };
    }

    // Use this for initialization
    void Start()
    {
        while (counter < m_AmountOfEnemy)
        {
            counter++;

            float x = Random.Range((float)-235.8, (float)-27.4);
            float z = Random.Range((float)121.5, (float)-57.6);

            //m_Goblins[counter].transform.position;
        }
    }
}