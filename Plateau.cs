using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace projet_algo
{
    public class Plateau
    {
        char[,] matrice;

        #region Constructeurs
        public Plateau(int ligne, int colonne)
        {
            List<char> listeLettre = ListeLettre("Lettre.txt", ligne, colonne);
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

        public Plateau()
        {
            matrice = null;
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
        
        public List<char> ListeLettre(string filename, int ligne, int colonne)
        {
            List<char> listeLettre = new List<char>();
            StreamReader sr = new StreamReader(filename);
            int nbrLettre = ligne * colonne;

            string line = sr.ReadLine();

            while (line != null)
            {
                string[] lineSplit = line.Split(',');
                int occurenceMax = int.Parse(lineSplit[1]) ; 

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
            try
            {
                bool verif = false;
                bool estMaj = false;
                mot = mot.ToLower();
                if (mot.Length < 2)
                {
                    Console.WriteLine("Erreur : Le mot doit être d'au moins 2 lettres.");
                    return false;
                }

                int colonnes = matrice.GetLength(1);

                // Parcourir chaque cellule de la base du plateau
                for (int j = 0; j < colonnes  && estMaj ==false; j++)
                {
                    // Recherche le mot à partir de chaque cellule de la base
                    if (Recherche_Lettre(mot, matrice.GetLength(0) - 1, j, 0))
                    {
                        verif = true;
                    }
                    if (verif)
                    {
                        Retire_Lettre(mot, matrice.GetLength(0) - 1, j, 0);
                        estMaj = true;
                    }
                }
                
                return verif;
            }
            catch(IndexOutOfRangeException e)
            {
                return false;
            }
            
        }

        public bool Recherche_Lettre(string mot, int ligne, int colonne, int index)
        {
            try
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
            catch( IndexOutOfRangeException e)
            {
                return false;
                throw;
            }
            
        }

        public bool Retire_Lettre(string mot, int ligne, int colonne, int index)
        {
            try
            {
                if (index == mot.Length)
                {
                    return true;
                }
                if (matrice[ligne,colonne] == mot[index])
                {
                    char memoire = matrice[ligne, colonne];
                    matrice[ligne, colonne] = ' ';
                    bool verif = Retire_Lettre(mot, ligne, colonne - 1, index + 1)|| Retire_Lettre(mot, ligne - 1, colonne - 1, index + 1)
                            || Retire_Lettre(mot, ligne - 1, colonne, index + 1) || Retire_Lettre(mot, ligne - 1, colonne + 1, index + 1)
                            || Retire_Lettre(mot, ligne, colonne + 1, index + 1) ;
                    return verif;
                }
                return false;
            }
            catch( IndexOutOfRangeException e)
            {
                return false;
                throw;
            }
            
        }

        public void GlisserLettres()
        {
            int rows = matrice.GetLength(0);
            int cols = matrice.GetLength(1);

            // Parcourir chaque colonne de la matrice
            for (int j = 0; j < cols; j++)
            {
                bool lettreDeplacee;
                do
                {
                    lettreDeplacee = false;

                    // Parcourir chaque ligne de bas en haut (sauf la première ligne)
                    for (int i = rows - 1; i > 0; i--)
                    {
                        if (matrice[i, j] == ' ' && matrice[i - 1, j] != ' ')
                        {
                            Console.Clear();
                            // Si la case est vide et la case au-dessus n'est pas vide, déplacer la lettre de la case au-dessus
                            matrice[i, j] = matrice[i - 1, j];
                            matrice[i - 1, j] = ' ';
                            lettreDeplacee = true;
                            Console.SetCursorPosition(0, Console.WindowHeight / 2 - Matrice.GetLength(0) / 2 - 1);
                            Interface.CenterText("Glissement en cours...");
                            Interface.AffichePlateau(Matrice);
                            Thread.Sleep(100);
                        }
                    }
                } while (lettreDeplacee);
            }
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
            if (matrice != null || matrice.Length!=0)
            {
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