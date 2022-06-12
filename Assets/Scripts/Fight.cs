using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class Fight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            EventManager.Instance.Raise(new PauseHasBeenPressEvent());
        }
    }
}