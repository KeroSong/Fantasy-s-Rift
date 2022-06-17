using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomEnemySystem : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        float x = Random.Range((float)-71.97, (float)-178);
        float z = Random.Range((float)-100, (float)115);

        Vector3 position = new Vector3(x, (float)5.00, z);
        this.transform.position = position;
    }
}