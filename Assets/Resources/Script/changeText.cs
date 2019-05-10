using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour
{
    public GameObject boomerang;
    public reproduceData script_boomerang;
    public string objName;
    // Start is called before the first frame update
    void Start()
    {
        boomerang = GameObject.Find("boomerang");
        script_boomerang = boomerang.GetComponent<reproduceData>();
         objName = this.gameObject.name;
    }

    // onClick method is called when pushing a button
    //This method change a button's text or a text explaining which dataset chose
    public void onClick()
    {
        if ("PlayButton"==objName)
        {
            if (true == script_boomerang.get_isPlay())
            {
                this.GetComponentInChildren<Text>().text = "stop";
            }
            else
            {
                this.GetComponentInChildren<Text>().text = "Start";
            }
        }
        else
        {
            if (1 == script_boomerang.get_whichSelectedData())
            {
                this.GetComponent<Text>().text = "Dataset 1 selected";
            }
            else if (2 == script_boomerang.get_whichSelectedData())
            {
                this.GetComponent<Text>().text = "Dataset 2 selected";
            }
        }
        
    }
}

