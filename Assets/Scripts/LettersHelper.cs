using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LettersHelper : MonoBehaviour
{
    private List<GameObject> FirstLettersList, SecondLettersList;
    private Sprite[] sprites;
    private Dictionary<string, string> letterwithimages = new Dictionary<string, string>();
    public Text UserWords;
    public GameObject Letters_0,
       Letters_1, Letters_2, Letters_3, Letters_4, Letters_5,
       Letters_6, Letters_7, Letters_8, Letters_9, Letters_10,
       Letters_11, Letters_12, Letters_13, Letters_14,
       Letters_15, Letters_16, Letters_17, Letters_18, Letters_19,
       Letters_20, Letters_21, Letters_22, Letters_23, Letters_24,
       Letters_25, Letters_26, Letters_27, Letters_28, Letters_29,
       Letters_30, Letters_31, Letters_32;
    public int lettersCount = 5,scale=100;



    // Start is called before the first frame update
    void Start()
    {
        
        // Выделяем в 2 листа гласные и согласные
        SetListofLetters();
        //расставляю в заданном порядке неповторяющиеся буквы, которые имет 2 гласные и 3 согласные в пропорциях к Scale
        SetLetters(scale);
        //Создается словарь ключ-значений, где название ключ название спрайта, а значение его значение в кирилице.
        SetDictionary();
        //Устанавливаем Текст "слова которые вы написали" 
        SetWordsWriter();
    }

   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
          //  WordsWrited.gameObject.AddComponent<Event>
            //Проверяем по какому объекту кликнул пользователь и заносим эту буквы в слово которое будем собирать 
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit)
            {

             string changes=  Getletter(hit.collider.gameObject.name);
             Debug.Log(hit.collider.gameObject.name);
             UserWords.text += changes;
                



            }
        }
    }


















    private void SetDictionary()
    {
        var templetters = new List<string>() { "Letters_0", "Letters_1", "Letters_2", "Letters_3", "Letters_4", "Letters_5",
            "Letters_6","Letters_7", "Letters_8", "Letters_9","Letters_10","Letters_11", "Letters_12", "Letters_13",
            "Letters_14", "Letters_15","Letters_16", "Letters_17", "Letters_18", "Letters_19",
            "Letters_20", "Letters_21","Letters_22", "Letters_23", "Letters_24","Letters_25",
            "Letters_26", "Letters_27","Letters_28", "Letters_29","Letters_30", "Letters_31", "Letters_32"
             };
    
       
        var tempalphabet = new List<string>() { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
        for (int i = 0; i < templetters.Count; i++)
        {
            letterwithimages.Add(templetters[i]+"(Clone)", tempalphabet[i]);
          
        }
    }
    private string Getletter(GameObject obj)
    { return letterwithimages[obj.name]; }
    private string Getletter(string str)
    { return letterwithimages[str]; }
    private void SetListofLetters()
    {
        FirstLettersList = new List<GameObject>() { Letters_0, Letters_5, Letters_6, Letters_9, Letters_15, Letters_20, Letters_28, Letters_30, Letters_31, Letters_32 };
        SecondLettersList = new List<GameObject>() { Letters_1, Letters_2, Letters_3, Letters_4, Letters_7, Letters_8,  Letters_10,Letters_11, Letters_12, Letters_13,
        Letters_14,Letters_16, Letters_17, Letters_18, Letters_19, Letters_21, Letters_22, Letters_23, Letters_24,Letters_25, Letters_26, Letters_27,  Letters_29,};
    }
    private void SetWordsWriter()
    {
        //UserWords.transform.position = new Vector3(0.5f, 4.5f, 1);
        //WordsWrited.GetComponent<TextMesh>().text = "*";
        //Instantiate(WordsWrited, new Vector2(0.5f, 3.5f), Quaternion.identity);
    }
    void SetLetters(float scale=1)
    {
        //for 5 let
        for (int i = -2; i <= 2; i++)
        {
            var temp = GetRandListLetter(i);
            float x= i*scale
                , y= getRightParams(i)*scale;
            Instantiate(temp, new Vector2(x, y), Quaternion.identity);
            if (i < 0) { FirstLettersList.Remove(temp); }
            else SecondLettersList.Remove(temp);
        }
    }

    private GameObject GetRandListLetter(int i)
    {

        if (i < 0)
        {
            int rand = UnityEngine.Random.Range(0, FirstLettersList.Count);

            return FirstLettersList[rand];
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, SecondLettersList.Count);
            return SecondLettersList[rand]; 
        }
    }

    private float getRightParams(int i)
    {
        //распологает буквы по заданной траектории что бы получилось подобие круга
        if (i % 3 == 0)
        {
            return (i % 2 == 0) ? -2 : 2;
        }
        return (i % 2 == 0) ? -1 : 1f;
    }
}
