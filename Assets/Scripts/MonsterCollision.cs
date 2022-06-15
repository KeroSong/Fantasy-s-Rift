using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("début du combat");
            /*Destroy(gameObject);*/
        }
    }

}
