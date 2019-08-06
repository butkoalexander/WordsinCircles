using System;
using System.Collections.Generic;
using System.IO;

namespace OptimizeDictionary
{

    class Program
    {
        

        static void Main(string[] args)
        {
            string[] createText=null;
            string path = @"C:\Users\Leikar\Documents\My Projects\WordsinCircles\OptimizeDictionary\word_rus.txt";
            string newpath = @"C:\Users\Leikar\Documents\My Projects\WordsinCircles\OptimizeDictionary\newword_rus.txt";
            string temp;
            // Open the file to read from.
            string[] readText = File.ReadAllLines(path);
            List<string> newText=new List<string>();
            foreach (string s in readText)
            {
                temp = "";
                for (int i = 0; i < s.Length; i++)
                {
                    if (!temp.Contains(s[i]))
                    {
                        temp += s[i];
                    }
                    
                }
                if(temp.Length<=5)
                {
                    newText.Add(s);
                }

                   
            }




          
                // Create a file to write to.
               
                File.WriteAllLines(newpath, newText);
           

        }
       
    }
}
