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

    public Text UserWords,TextScore,lastone,lasttwo,lastthree;
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
      
        SetXMLLevels(); // доставем из хмл уровни
        SetListofLetters();  //Создаем соответсвие картинка-буква
        
        CurrentWordHash = SetLetters(scale);//расставляем в заданном порядке неповторяющиеся буквы, в пропорциях к Scale
        
        SetDictionary();//Создается словарь ключ-значений, где название ключ название спрайта, а значение его значение в кирилице.
      
                
    }

    private void SetListofLetters()
    {
        //Создаем соответсвие картинка-буква
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
         
            //Проверяем по какому объекту кликнул пользователь и заносим эту букву в слово которое будем собирать 
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit)
            {
                //Основной процесс приложения
                GameProcess(hit);

            }
        }
    }

    private void GameProcess(RaycastHit2D hit)
    {
        string changes = Getletter(hit.collider.gameObject.name);// Получаем нажатую букву
        UserWords.text += changes;//добавляем букву на экран
        if (dict[CurrentWordHash].Contains(UserWords.text))//Проверяем существование текущего составленного слова
        {
            //Если такое слово существует
            intScore += UserWords.text.Length;//Увеличиваем количество очков за это слово
            TextScore.text = "Score: " + intScore;//Выводим очки на экран
            SetNewWordinScore(UserWords.text);//Обновляем список последних 3 слов на экаране

            dict[CurrentWordHash].Remove(UserWords.text);//Убираем это слово из словаря, что бы мы на него больше не могли наткнуться еще раз
            UserWords.text = "";//обнуляем поле для ввода, что бы пользователь мог начать составлять новое слово

            if (dict[CurrentWordHash].Count == 0)//Если слова которые возможно составить из данного состава букв кончились убираем этот ключ из словаря
            {
                dict.Remove(CurrentWordHash);
                IsNewKeyNeeded = true;//Ставим флаг о необходимости достать новый ключ
            }
        }

        if (IsNewKeyNeeded)
        {
            ClearListofImages();//Убираем старые буквы с экрана
            CurrentWordHash = SetLetters(scale);//Ставим новые буквы на экран
        }
    }

    private void SetNewWordinScore(string str)
    {
       if(lasttwo!=null)
        {
            lastthree.text = lasttwo.text;
        }
       if(lastone!=null)
        {
            lasttwo.text = lastone.text;
        }
        lastone.text = str+": "+str.Count();
    }

    private void ClearListofImages()
    {
        foreach (var item in ListOfLettersImages)
        {
            item.SetActive(false);
            Destroy(item);
        }
    }

    private  void SetXMLLevels()
    {
        //NewOptimizedXML
        // Подгружаем файл с уровнями, предварительно сформированными
        dict = new Dictionary<string, List<string>>();
       ReadObject(@"C:\Users\Leikar\Documents\My Projects\WordsinCircles\Assets\Resources\NewOptimizedXML2.xml");
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

    private string Getletter(string str)
    { return letterwithimages[str]; }
    private GameObject Getimage(char str)
    {
        return lettersWithGameObjects[str.ToString()];
    }
   
 
    string SetLetters(float scale=1)
    {
        if (!IsNewKeyNeeded)
        {
            throw new Exception("IsNewKeyNeeded :"+ IsNewKeyNeeded);
        }

        ListOfLettersImages = new List<GameObject>();
        string temp2= RandomKeys(dict).ToString();//получаем случайный ключ изсловаря уровней
        char[] a = temp2.ToCharArray();
        for (int i = -2; i <= 2; i++)
        {
           
           float x= i*scale
               , y= GetRightParams(i)*scale;//Расставляем 5 букв по кругу (примерно) 

            ListOfLettersImages.Add(Instantiate(Getimage(a[i+2]), new Vector2(x, y), Quaternion.identity));
        
        }
        IsNewKeyNeeded = false;
        return temp2;
    }
    

    private float GetRightParams(int i)
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
        //Десириализуем файл уровней в систему словаря Dictionary<string, List<string>>,
        //где ключ это набор уникальных букв(5), и значение список возможных вариантов составленных слов
        //данный файл формируется в другой программе
        FileStream fs = new FileStream(fileName,FileMode.Open);
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
        Random rand = new Random();//Получаем случайный ключ
        List<string> stringkeys = _dict.Keys.ToList();
        int size = stringkeys.Count-1;
        int keynomber = rand.Next(size);
        return stringkeys[keynomber];
        
    }
}