using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refreshbutton : MonoBehaviour
{
    public GameObject textToRefresh;
    public void Clickedrefresh()
    {
        print("Onclick");
        if(textToRefresh)
        textToRefresh.GetComponent<Text>().text = "";
    }
}

