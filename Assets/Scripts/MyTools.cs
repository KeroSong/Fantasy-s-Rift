using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyTools
{
    public static void ColorizeRandom(GameObject go)
    {
        MeshRenderer mr = go.GetComponentInChildren<MeshRenderer>();
        if(mr)
        {
            mr.material.color = Random.ColorHSV();
        }
    }

    public static void LOG(this Component component,string msg)
    {
        Debug.Log($"{Time.frameCount} | {Time.time:N02} | {component.name} | {component.GetType()} | {msg}");
    }
}
