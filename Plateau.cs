using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Plateau
    {
        char[,] matrice;

        public Plateau(int taille)
        {
            List<char> listeLettre = ListeLettre("Lettre.txt");
            Random rand = new Random();

            matrice = new char[taille, taille];

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    int index = rand.Next(listeLettre.Count);
                    matrice[i, j] = listeLettre[index];
                    listeLettre.RemoveAt(index);
                }
            }

        }

        public Plateau(string filename)
        {
            List<char[]> listeLettre = new List<char[]>();

            StreamReader sr = new StreamReader(filename);
            string s = sr.ReadLine();

            while (s != null)
            {
                string[] lines = s.Split(';');
                char[] tabLettre = new char[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    tabLettre[i] = char.Parse(lines[i]);
                }
                listeLettre.Add(tabLettre);
                s = sr.ReadLine();
            }
            sr.Close();

            matrice = new char[listeLettre.Count,listeLettre[0].Length];
            
            for (int i = 0; i <listeLettre.Count ; i++)
            {
                for (int j = 0; j <listeLettre[0].Length ; j++)
                {
                    matrice[i, j] = listeLettre[i][j];
                }
            }
        }

        public char[,] Matrice
        {
            get { return matrice; }
            set { matrice = value; }
        }
        
        public List<char> ListeLettre(string filename)
        {
            List<char> listeLettre = new List<char>();
            StreamReader sr = new StreamReader(filename);

            string line = sr.ReadLine();

            while (line != null)
            {
                string[] lineSplit = line.Split(',');

                int occurenceMax = int.Parse(lineSplit[1]); 

                for (int i = 0; i < occurenceMax; i++)
                {
                    char lettre = char.Parse(lineSplit[0].ToLower());
                    listeLettre.Add(lettre);
                }
                line = sr.ReadLine();
            }
            sr.Close();

            return listeLettre;
        }
    }
}