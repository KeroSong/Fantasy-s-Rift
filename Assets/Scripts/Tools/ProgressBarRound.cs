using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarRound : MonoBehaviour
{
    [SerializeField] string MechNumber;
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
    void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        txt = bar.transform.Find("Text").GetComponent<Text>();
        Val = 0; 
        txt.text = MechNumber;
    }

    // Update is called once per frame
    void UpdateValue()
    {
        
        bar.fillAmount = val / 100;
    }

    private void Update()
    {
        
    }
}
