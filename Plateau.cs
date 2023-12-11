using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace projet_algo
{
    public class Plateau
    {
        #region Attributs
        /// <summary> Matrice de caractères représentant le plateau de jeu </summary>
        char[,] matrice;
        #endregion

        #region Constructeurs

        /// <summary> Constructeur du plateau de jeu </summary>
        /// <param name="ligne"> Nombre de lignes du plateau </param>
        /// <param name="colonne"> Nombre de colonnes du plateau </param>
        /// <returns> Un nouveau plateau de jeu </returns>
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

        /// <summary> Constructeur du plateau de jeu </summary>
        /// <retuen> Un nouveau plateau de jeu </returns>
        public Plateau()
        {
            matrice = null;
        }

        /// <summary> Constructeur du plateau de jeu </summary>
        /// <param name="filename"> Nom du fichier texte </param>
        /// <returns> Un nouveau plateau de jeu </returns>
        public Plateau(string filename)
        {
            ToRead(filename);
        }
        #endregion

        #region Propriétés
        /// <summary> Propriété de la matrice de caractères représentant le plateau de jeu </summary>
        public char[,] Matrice
        {
            get { return matrice; }
            set { matrice = value; }
        }
        #endregion

        #region Méthodes

        /// <summary> Méthode qui retourne la liste de lettres </summary>
        /// <param name="filename"> Nom du fichier texte </param>
        /// <param name="ligne"> Nombre de lignes du plateau </param>
        /// <param name="colonne"> Nombre de colonnes du plateau </param>
        /// <returns> La liste de lettres </returns>
        public List<char> ListeLettre(string filename)
        {
            List<char> listeLettre = new List<char>();
            StreamReader sr = new StreamReader(filename);

            string line = sr.ReadLine();

            // Remplie la liste de lettres selon leur nombre d'occurence max
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

        /// <summary> Methodes qui écrit la matrice dans un fichier texte </summary>
        /// <param name="nomFile"> Nom du fichier texte </param>
        /// <returns> La matrice dans un fichier texte </returns>
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

        /// <summary> Méthode qui recherche un mot dans la matrice </summary>
        /// <param name="mot"> Mot à rechercher </param>
        /// <returns> Si le mot est dans la matrice </returns>
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

                // Recherche le mot à partir de la base et s'arrète s'il a parcouru toute la base ou quand le mot est trouvé
                for (int j = 0; j < colonnes  && estMaj ==false; j++)
                {
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

        /// <summary> Méthode qui recherche les lettres du mots dans la matrice </summary>
        /// <param name="mot"> Mot à rechercher </param>
        /// <param name="ligne"> Ligne de la matrice </param>
        /// <param name="colonne"> Colonne de la matrice </param>
        /// <param name="index"> Index du mot </param>
        /// <returns> Si la lettre est dans la matrice </returns>
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
            }
            
        }

        /// <summary> Méthode qui retire les lettres du mots dans la matrice </summary>
        /// <param name="mot"> Mot à retirer </param>
        /// <param name="ligne"> Ligne de la matrice </param>
        /// <param name="colonne"> Colonne de la matrice </param>
        /// <param name="index"> Index du mot </param>
        /// <returns> Si la lettre est retirée </returns>
        public bool Retire_Lettre(string mot, int ligne, int colonne, int index)
        {
            // Meme principe que la méthode Recherche_Lettre mais on retire les lettres en plus
            try
            {
                if (index == mot.Length)
                {
                    return true;
                }
                if (matrice[ligne,colonne] == mot[index])
                {
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
            }
            
        }

        /// <summary> Methodes qui fait glisser les mots vers le bas </summary>
        /// <returns> La matrice avec les mots glissés </returns>
        public void GlisserLettres()
        {
            int ligne = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);

            // Parcourir chaque colonne de la matrice
            for (int j = 0; j < colonne; j++)
            {
                bool lettreDeplacee;
                do
                {
                    lettreDeplacee = false;

                    // Parcourir chaque ligne de bas en haut (sauf la première ligne)
                    for (int i = ligne - 1; i > 0; i--)
                    {
                        if (matrice[i, j] == ' ' && matrice[i - 1, j] != ' ')
                        {
                            Console.Clear();
                            // Si la case est vide et la case au-dessus n'est pas vide, déplacer la lettre de la case au-dessus
                            matrice[i, j] = matrice[i - 1, j];
                            matrice[i - 1, j] = ' ';
                            lettreDeplacee = true;

                            // Afficher le glissement du plateau
                            Console.SetCursorPosition(0, Console.WindowHeight / 2 - Matrice.GetLength(0) / 2 - 1);
                            Interface.CenterText("Glissement en cours...");
                            Interface.AffichePlateau(Matrice);
                            Thread.Sleep(200);
                        }
                    }
                } while (lettreDeplacee);
            }
        }
        
        /// <summary> Méthode qui lit un fichier contenant une matrice</summary>
        /// <param name="nomFile"> Nom du fichier texte </param>
        /// <returns> La matrice </returns>
        public void ToRead(string nomFile)
        {
            List<char[]> listeLettre = new List<char[]>();

            StreamReader sr = new StreamReader(nomFile);
            string s = sr.ReadLine();

            // Met chaque ligne du fichier dans une liste de char
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

            // Initialise la matrice avec la liste de char
            matrice = new char[listeLettre.Count,listeLettre[0].Length];
            
            // Rempli la matrice avec la liste de char
            for (int i = 0; i <listeLettre.Count ; i++)
            {
                for (int j = 0; j <listeLettre[0].Length ; j++)
                {
                    matrice[i, j] = listeLettre[i][j];
                }
            }
        }
        
        /// <summary> Méthode qui vérifie si la matrice est vide </summary>
        /// <returns> Si la matrice est vide </returns>
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
        
        /// <summary> Méthode qui affiche la matrice </summary>
        /// <returns> La matrice </returns>
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
        #endregion
    }
}