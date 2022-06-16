using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    Image bar;
    Text txt;

    private float val;

    public float Val
    {
        get
        {
            return val; 
        }

        set
        {
            val = value;
            val = Mathf.Clamp(val,0,100);
            UpdateValue();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        txt = bar.transform.Find("Text").GetComponent<Text>();
        Val = 100;
    }

    // Update is called once per frame
    void UpdateValue()
    {
        txt.text = val + "%";
        bar.fillAmount = val / 100;
    }

    private void Update()
    {
        
    }
}
