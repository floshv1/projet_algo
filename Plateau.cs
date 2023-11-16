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
            int ligne = 0;
            int colonne = 0;

            string[] lines = File.ReadAllLines(filename); // Fait comme si toutes les lignes etait egales
            string[] firstLine = lines[0].Split(';');

            ligne = firstLine.Length;
            colonne = lines.Length;

            matrice = new char[ligne, colonne];

            for (int i = 0; i < ligne; i++)
            {
                string[] line = lines[i].Split(';');

                for (int j = 0; j < colonne ; j++)
                {
                    matrice[i, j] = char.Parse(line[j]);
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