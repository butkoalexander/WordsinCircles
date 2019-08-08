using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Xml;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Random = System.Random;
using System.Linq;

public class LettersHelper : MonoBehaviour
{
    private List<GameObject> ListOfLettersImages=null;
    private string CurrentWordHash="";
    private int intScore=0;
    private bool IsNewKeyNeeded=true;
    private Dictionary<string, string> letterwithimages = new Dictionary<string, string>();
    private Dictionary<string, GameObject> lettersWithGameObjects = new Dictionary<string, GameObject>();

    public Text UserWords,TextScore;
    public Dictionary<string, List<string>> dict;
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
       
        SetXMLLevels();
       
        // Выделяем в 2 листа гласные и согласные**** было необходиимо для полностью рандомного вызова - не практично
        SetListofLetters();
        //расставляю в заданном порядке неповторяющиеся буквы, которые имет 2 гласные и 3 согласные в пропорциях к Scale
        CurrentWordHash = SetLetters(scale);
        //Создается словарь ключ-значений, где название ключ название спрайта, а значение его значение в кирилице.
        SetDictionary();
        //Устанавливаем Текст "слова которые вы написали" 
       // SetWordsWriter();
        //Подгружаем уровни из хмл в dict
        
    }

    private void SetListofLetters()
    {
        
       var LettersList = new List<GameObject>(){ Letters_0,
        Letters_1, Letters_2, Letters_3, Letters_4, Letters_5,
        Letters_6, Letters_7, Letters_8, Letters_9, Letters_10,
        Letters_11, Letters_12, Letters_13, Letters_14,
        Letters_15, Letters_16, Letters_17, Letters_18, Letters_19,
        Letters_20, Letters_21, Letters_22, Letters_23, Letters_24,
        Letters_25, Letters_26, Letters_27, Letters_28, Letters_29,
        Letters_30, Letters_31, Letters_32 };


        var tempalphabet = new List<string>() { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
        for (int i = 0; i < LettersList.Count; i++)
        {
            lettersWithGameObjects.Add( tempalphabet[i], LettersList[i]);
        }
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
             if(dict[CurrentWordHash].Contains(UserWords.text))
                {
                    intScore += UserWords.text.Length;
                    TextScore.text = "Score: " + intScore;
                    UserWords.text = "";
                    dict[CurrentWordHash].Remove(UserWords.text);
                    if (dict[CurrentWordHash].Count == 0)
                    {
                        dict.Remove(CurrentWordHash);
                        IsNewKeyNeeded = true;
                    }
                }



            }
        }
    }















    private  void SetXMLLevels()
    {
        //NewOptimizedXML
        dict = new Dictionary<string, List<string>>();
       ReadObject(@"C:\Users\Leikar\Documents\My Projects\WordsinCircles\Assets\Resources\NewOptimizedXML.xml");
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
    private GameObject Getimage(char str)
    {
        return lettersWithGameObjects[str.ToString()];
    }
   
    private void SetWordsWriter()
    {
       
    }
    string SetLetters(float scale=1)
    {
        if (!IsNewKeyNeeded)
        {
            throw new Exception("IsNewKeyNeeded :"+ IsNewKeyNeeded);
        }
        string temp2= RandomKeys(dict).ToString();
        char[] a = temp2.ToCharArray();
        for (int i = -2; i <= 2; i++)
        {
           
           float x= i*scale
               , y= getRightParams(i)*scale;

            ListOfLettersImages.Add(Instantiate(Getimage(a[i+2]), new Vector2(x, y), Quaternion.identity));
        
        }
        IsNewKeyNeeded = false;
        return temp2;
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

    public void ReadObject(string fileName)
    {
        Console.WriteLine("Deserializing an instance of the object.");
        FileStream fs = new FileStream(fileName,
        FileMode.Open);
        XmlDictionaryReader reader =
            XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
        DataContractSerializer ser = new DataContractSerializer(typeof(Dictionary<string, List<string>>));

        // Deserialize the data and read it from the instance.
        Dictionary<string, List<string>> deserializedPerson =
            (Dictionary<string, List<string>>)ser.ReadObject(reader, true);
        reader.Close();
        fs.Close();
        dict = deserializedPerson;
      
    }

    public string RandomKeys(Dictionary<string, List<string>> _dict)
    {
        Random rand = new Random();
        List<string> stringkeys = _dict.Keys.ToList();
        int size = stringkeys.Count-1;
        int keynomber = rand.Next(size);
        return stringkeys[keynomber];
        
    }
}
//использовалось для рандомных букв на поле **** не практично
    //private GameObject GetRandListLetter(int i)
    //{

    //    if (i < 0)
    //    {
    //        int rand = UnityEngine.Random.Range(0, FirstLettersList.Count);

    //        return FirstLettersList[rand];
    //    }
    //    else
    //    {
    //        int rand = UnityEngine.Random.Range(0, SecondLettersList.Count);
    //        return SecondLettersList[rand]; 
    //    }
    //} 
    //  private void SetListofLetters()
    //{
    //     LettersList = new List<GameObject>(){ Letters_0,
    //    Letters_1, Letters_2, Letters_3, Letters_4, Letters_5,
    //    Letters_6, Letters_7, Letters_8, Letters_9, Letters_10,
    //    Letters_11, Letters_12, Letters_13, Letters_14,
    //    Letters_15, Letters_16, Letters_17, Letters_18, Letters_19,
    //    Letters_20, Letters_21, Letters_22, Letters_23, Letters_24,
    //    Letters_25, Letters_26, Letters_27, Letters_28, Letters_29,
    //    Letters_30, Letters_31, Letters_32 };
    //    FirstLettersList = new List<GameObject>() { Letters_0, Letters_5, Letters_6, Letters_9, Letters_15, Letters_20, Letters_28, Letters_30, Letters_31, Letters_32 };
    //    SecondLettersList = new List<GameObject>() { Letters_1, Letters_2, Letters_3, Letters_4, Letters_7, Letters_8,  Letters_10,Letters_11, Letters_12, Letters_13,
    //    Letters_14,Letters_16, Letters_17, Letters_18, Letters_19, Letters_21, Letters_22, Letters_23, Letters_24,Letters_25, Letters_26, Letters_27,  Letters_29,};
    //}