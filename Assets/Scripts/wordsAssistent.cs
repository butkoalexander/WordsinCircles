using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class wordsAssistent : MonoBehaviour
{
    public Text text;
    private 
    // Start is called before the first frame update
    void Start()
    {
        //StreamReader str = new StreamReader("Assets/Resources/word_rus.txt");
        //while (!str.EndOfStream)
        //{
        //    Debug.Log("reader");
        //    string st = str.ReadLine();
        //}

        //    //StreamReader str = new StreamReader("Assets/Resources/word_rus.txt");
        //    //while (!str.EndOfStream)
        //    //{
        //    //    //
        //    //}


    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0)&&text!=null&&text.text!=null)
        {
            
          
            
        }
    }
}
