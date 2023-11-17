using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dictionnaire
{
    public class Dictionnaire
    {
        string langue;
        list<string[]> dico;

        public Dictionnaire(string fileName)
        {
            
        }

        public list<string[]> ReadFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();




        }

    }
}