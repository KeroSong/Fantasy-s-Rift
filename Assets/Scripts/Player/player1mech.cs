using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1mech : MonoBehaviour
{
    Animator MechAnimator;

    private void Awake()
    {
        MechAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isDead()
    {
        MechAnimator.SetTrigger("dead");
        GetComponent<player1mech>().enabled = false;
    }
}
