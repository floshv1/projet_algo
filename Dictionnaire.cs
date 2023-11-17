using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Dictionnaire
    {
        string langue;
        List<string[]> dico;

        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string fileName = "Mots_" + langue + ".txt";
            ReadFile(fileName);
        }

        public void ReadFile(string fileName)
        {
            dico = new List<string[]>();
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();

            while (line != null)
            {
                string[] words = line.Split(" ");
                dico.Add(words);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public bool RecheDichoRecursif(string mot)
        {
            bool trouve = false;
            int lettre = Convert.ToInt32(mot[0]);
            int indexLettre = lettre - 97;

            return trouve;
        }

        public void Tri_Fusion()
        {
            for (int i = 0; i < dico.Count; i++)
            {
                
            }
        }

        public string toString()
        {
            int nombreDeMots = 0;
            foreach (string[] words in dico)
            {
                nombreDeMots += words.Length;
            }
            string strNombreDeMots= "Il y a " + nombreDeMots + " mots dans le dictionnaire.";
            return strNombreDeMots + "\n" + "La langue du dictionnaire est " + langue + ".";
        }

    }
}