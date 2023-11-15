using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Plateau
    {
        char[,] matrice;

        public Plateau(int ligne, int colonne)
        {
            matrice = new char[ligne, colonne];
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
        

         
    }
}