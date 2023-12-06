using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Plateau
    {
        char[,] matrice;

        #region Constructeurs
        public Plateau(int ligne, int colonne)
        {
            List<char> listeLettre = ListeLettre("Lettre.txt");
            Random rand = new Random();

            matrice = new char[ligne, colonne];

            for (int i = 0; i < ligne; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    int index = rand.Next(listeLettre.Count);
                    matrice[i, j] = listeLettre[index];
                    listeLettre.RemoveAt(index);
                }
            }

        }

        public Plateau(string filename)
        {
            ToRead(filename);
        }
        #endregion

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

        public void ToFile(string nomFile)
        {
            StreamWriter sw = new StreamWriter(nomFile);
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    sw.Write(matrice[i, j] + ";");
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        public bool Recherche_Mot(string mot)
        {
            bool verif = false;
            mot = mot.ToLower();
            if (mot.Length < 2)
            {
                Console.WriteLine("Erreur : Le mot doit être d'au moins 2 lettres.");
            }

            int colonnes = matrice.GetLength(1);

            // Parcourir chaque cellule de la base du plateau
            for (int j = 0; j < colonnes; j++)
            {

                
                // Recherche le mot à partir de chaque cellule de la base
                if (Recherche_Lettre(mot, matrice.GetLength(0) - 1, j, 0))
                {
                    verif = true;
                }
            }

            if(!verif)
            {
                Console.WriteLine($"Erreur : Le mot '{mot}' n'est pas dans le plateau.");

            }
            
            return verif;
        }

        public bool Recherche_Lettre(string mot, int ligne, int colonne, int index)
        {
            if (index == mot.Length)
            {
                return true;
            }
            if (matrice[ligne,colonne] == mot[index])
            {
                char memoire = matrice[ligne, colonne];
                bool verif = Recherche_Lettre(mot, ligne, colonne - 1, index + 1)|| Recherche_Lettre(mot, ligne - 1, colonne - 1, index + 1)
                        || Recherche_Lettre(mot, ligne - 1, colonne, index + 1) || Recherche_Lettre(mot, ligne - 1, colonne + 1, index + 1)
                        || Recherche_Lettre(mot, ligne, colonne + 1, index + 1) ;
                return verif;
            }
            return false;
        }
 
        public void ToRead(string nomFile)
        {
            List<char[]> listeLettre = new List<char[]>();

            StreamReader sr = new StreamReader(nomFile);
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

        public bool estVide()
        {
            bool verif = true;
            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for(int j = 0; j < matrice.GetLength(1); j++)
                {
                    if(matrice[i,j] != ' ')
                    {
                        verif = false;
                    }
                }
            }
            return verif;
        }
        public string toString()
        {
            string texte = "";
            for(int i = 0; i < this.matrice.GetLength(0); i++)
            {
                for(int j = 0; j < this.matrice.GetLength(1); j++)
                {
                    texte += matrice[i, j] + " ";
                }
                texte += "\n";
            }
            return texte;
        }
    
    }
}