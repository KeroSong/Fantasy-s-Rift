using System;
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

    public void attack()
    {
        MechAnimator.SetBool("attack", true);

    }

    public void stopAttack()
    {
        MechAnimator.SetBool("attack", false);

    }

    public void isDead()
    {
        MechAnimator.SetTrigger("dead");
        GetComponent<player1mech>().enabled = false;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
    }
}
