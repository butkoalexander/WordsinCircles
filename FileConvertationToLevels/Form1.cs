using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace FileConvertationToLevels
{
   



    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> dict;
        public Form1()
        {
            InitializeComponent();
            dict = new Dictionary<string, List<string>>();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<string> readText = new List<string>();
        
            string tmp = null;
            if (textBox1.Text != null)
            {
                try
                {
                    readText = File.ReadAllLines(textBox1.Text).ToList<string>();
                }
                catch (Exception exp) { MessageBox.Show(exp.Message); }

                for (int i = 0; i<readText.Count; i++)
                {
                    tmp = Likeheshcheck(readText[i]);//получаем "хэшкод" ключа, хэшкод создается из уникальных отсортированных по порядку букв из данного слова

                    //Если в "хэшкоде" уникальных букв 5 и такого ключа еще не существует добавляем новый ключ и слово к этому ключу,                                 
                    //при этом удаляем его из перепроверочного списка, (перепроверочный список нужен на случай если будет слово, из которого нельзя создать ключ,
                    //и мы не можем его сейчас привязать ни к одному существующему ключу
                    if (tmp.Length == 5)
                    {
                        if (!dict.ContainsKey(tmp))
                        {
                            dict.Add(tmp, null);

                        }
                        dict[tmp] = new List<string>
                            {
                                readText[i]
                            };
                        readText.Remove(readText[i]);
                    }
                    //else
                    //{//Если уникальных букв в "хэшкоде" меньше 5 значит мы не можем создать из него ключ(потому что ключ это в будующем первоначальный сэтап уровня на 5 букв)
                    //}
                }
                // после данной операции в readtext остаются слова только с хэшкодом меньше 5 букв, теперь имея ключи проходимся по ним и значениям и добавляем их к ключам
                foreach (var item in dict.Keys)
                {
                    for (int i = 0; i < readText.Count; i++)
                    {
                        if(LikehashEquals(item,readText[i]))//Если больший хэш код (ключа ), содержит все буквы хэш кода меньшего добавляем значение к этому ключу!
                        {
                            dict[item].Add(readText[i]);
                        }
                    }
                }
                // Теперь Словарь dict содержит в себе реальные уровни, с возможными исходами наборов слов.
                //Записываем в file путем сюриализации
               
                WriteObject(Path.GetDirectoryName(textBox1.Text) + "\\NewOptimizedXML.xml",dict);
               

            }

        }
        public static void WriteObject(string fileName, Dictionary<string, List<string>> dict)
        {
            
            FileStream writer = new FileStream(fileName, FileMode.Create);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(Dictionary<string, List<string>>));
            ser.WriteObject(writer, dict);
            writer.Close();
            MessageBox.Show("DONE");
        }
        //public static void ReadObject(string fileName)
        //{
        //    Console.WriteLine("Deserializing an instance of the object.");
        //    FileStream fs = new FileStream(fileName,
        //    FileMode.Open);
        //    XmlDictionaryReader reader =
        //        XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
        //    DataContractSerializer ser = new DataContractSerializer(typeof(Person));

        //    // Deserialize the data and read it from the instance.
        //    Person deserializedPerson =
        //        (Person)ser.ReadObject(reader, true);
        //    reader.Close();
        //    fs.Close();
        //    Console.WriteLine(String.Format("{0} {1}, ID: {2}",
        //    deserializedPerson.FirstName, deserializedPerson.LastName,
        //    deserializedPerson.ID));
        //}
        private string Likeheshcheck(string s)
        {
            
          s=GetMyHash(s);
            s.ToList<char>().Sort();
           return s.ToString();
        }
        private string GetMyHash(string s)
        {
            string temp = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (!temp.Contains(s[i]))
                {
                    temp += s[i];
                }

            }
            return temp;
        }
        private bool LikehashEquals(string sKey,string sValue)
        {
            sValue = Likeheshcheck(sValue);//преобразуем слово в хэш и проверяем его буквы на содержание в данном key
            for (int i = 0; i < sValue.Length; i++)
            {
                if (sValue[i]!=sKey[i]) return false;
            }
            return true;
            
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text= openFileDialog1.FileName;
        }
    }
    













    
}
