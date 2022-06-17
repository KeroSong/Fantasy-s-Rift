using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class DragonTerrorBringerCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.Instance.Raise(new FightTerrorBringerCollisionEvent());
        }
    }

}