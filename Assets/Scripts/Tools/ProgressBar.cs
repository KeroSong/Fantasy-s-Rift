using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    Image bar;
    Text txt;

    private float val;
    private float val2;

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

    public float Val2
    {
        get
        {
            return val2; 
        }

        set
        {
            val2 = value;
            val2 = Mathf.Clamp(val2,0,100);
            UpdateValue();
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        bar = this.transform.GetChild(0).GetComponent<Image>();
        txt = bar.transform.GetChild(0).GetComponent<Text>();
        int Id = this.transform.GetComponent<AttributeAId>().ID;
        if (Id == 1)
        {
            Val = PlayerPrefs.GetFloat("Santé") + PlayerPrefs.GetFloat("Santé_AI1");
            if (PlayerPrefs.GetInt("classe") == 0)
            {
                Val2 = 40;
            }
            else if (PlayerPrefs.GetInt("classe") == 1)
            {
                Val2 = 35;
            }
            else
            {
                Val2 = 30;
            }
        }
        else if (Id == 2)
        {
            Val = PlayerPrefs.GetFloat("Santé_AI2") + PlayerPrefs.GetFloat("Santé_AI3");
                Val2 = 25;
        }
        else if (Id == 3)
        {
            if (PlayerPrefs.GetString("Ennemi") == "Gobelin")
            {
                Val = 25;
                Val2 = Val;
            }
            else if (PlayerPrefs.GetString("Ennemi") == "SoulEater")
            {
                Val = 100;
                Val2 = Val;
            }
            else if (PlayerPrefs.GetString("Ennemi") == "TheNightmare")
            {
                Val = 100;
                Val2 = Val;
            }
            else if (PlayerPrefs.GetString("Ennemi") == "TerrorBringer")
            {
                Val = 100;
                Val2 = Val;
            }
            else
            {
                Val = 100;
                Val2 = Val;
            }
        }
        else if (Id == 4)
        {
            Val = 25;
            Val2 = Val;
        }
        else
        {
            Val = 25;
            Val2 = Val;
        }
    }

    // Update is called once per frame
    void UpdateValue()
    {
        txt.text = val + "PV";
        bar.fillAmount = val / val2;
    }

}
