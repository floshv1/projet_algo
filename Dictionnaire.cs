using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace projet_algo
{
    public class Dictionnaire
    {
        #region Attributs
        /// <summary> Langue du dictionnaire </summary>
        string langue;
        /// <summary> Liste de mots du dictionnaire </summary>
        List<string[]> listeMots;
        #endregion
        
        #region Constructeurs
        /// <summary> Constructeur du Dictionnaire</summary>
        /// <param name="langue"> Langue du dictionnaire </param>
        /// <returns> Un nouveau dictionnaire </returns>
        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string fileName = "Mots_" + langue + ".txt";
            if (File.Exists(fileName))
            {
                ReadFile(fileName);
            }
            else
            {
                Interface.CenterText($"Le fichier {fileName} n'existe pas");
            }
        }
        #endregion

        #region Propriétés
        /// <summary> Propriété de la liste de mots du dictionnaire </summary>
        public List<string[]> ListeMots 
        { 
            get{ return listeMots;}
        }
        /// <summary> Propriété de la langue du dictionnaire </summary>
        public string Langue
        {
            get { return langue; }
        }
        #endregion

        #region Méthodes
        /// <summary> Méthode qui lit le fichier texte et qui ajoute les mots dans la liste de mots </summary>
        /// <param name="fileName"> Nom du fichier texte </param>
        /// <return> Lecture du fichier </return>
        public void ReadFile(string fileName)
        {
            listeMots = new List<string[]>();
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();

            // On lit le fichier ligne par ligne
            // On ajoute chaque ligne dans la liste de mots, une ligne correspond a une lettre
            while (line != null)
            {
                string[] words = line.Split(' ');
                listeMots.Add(words);
                line = sr.ReadLine();
            }
            sr.Close();
        }
        
        /// <summary> Méthode qui recherche un mot dans le dictionnaire </summary>
        /// <param name="mot"> Mot à rechercher </param>
        /// <returns> Si le mot est dans le dictionnaire </returns>
        public bool RecheDichoDico(string mot)
        {
            
            bool lettreTrouve = false;
            int index = 0;
            if (mot.Length != 0 && mot != null)
            {
                // On met le mot en majuscule pour pouvoir le comparer avec mots de la liste
                mot= mot.ToUpper();
                for (int i = 0; i < listeMots.Count; i++)
                {
                    // On vérifie si la première lettre du mot est la même que la première lettre du tableau
                    if (mot[0] == listeMots[i][0][0])
                    {
                    lettreTrouve = true;
                    index = i;
                    }
                }   
            }
            // Si la lettre n'est pas dans le dictionnaire, on retourne faux
            if (lettreTrouve == false || mot == null || mot.Length == 0)
            {
                return false;
            }
            else
            {
                return RecheDichoRecursif(listeMots[index], mot, 0, listeMots[index].Length - 1);
            }
        }
        
        /// <summary> Méthode qui recherche un mot dans un tableau de mots </summary>
        /// <param name="tabMots"> Tableau de mot qui commence par la meme lettre</param>
        /// <param name="mot"> mot à trouver </param>
        /// <param name="debut"> indice de debut du tableau </param>
        /// <param name="fin"> indice de fin du tablaeu </param>
        /// <returns> Si le mot est dans le tableau</returns>
        public bool RecheDichoRecursif(string[] tabMots, string mot, int debut, int fin)
        {
            // Si le tableau est vide, on retourne faux
            if (fin < debut){
                return false;
            }

            // On cherche le milieu du tableau
            int milieu = (debut + fin) / 2;

            // On compare le mot avec le mot du milieu du tableau
            if (mot == tabMots[milieu])
            {
                return true;
            }
            // Si le mot est plus petit que le mot du milieu, on cherche dans la moitié gauche du tableau
            else if (tabMots[milieu].CompareTo(mot) > 0)
            {
                fin = milieu - 1;
            }
            // Sinon on cherche dans la moitié droite du tableau
            else
            {
                debut = milieu + 1;
            }
            return RecheDichoRecursif(tabMots, mot, debut, fin);        
        }
        
        /// <summary> Méthode qui trie les mots du dictionnaire </summary>
        /// <returns> Liste de mots triés </returns>
        public void Tri_Fusion_Dico()
        {
            for (int i = 0; i < listeMots.Count; i++)
            {
                Tri_Fusion(listeMots[i]);
            }
        }

        /// <summary>  Méthode qui trie un tableau de mots par ordre alphabétique </summary>
        /// <param name="tableau"> Tableau de mot qui commence par la meme lettre</param>
        /// <returns> Tableau trié </returns>
        public string[] Tri_Fusion(string[] tableau)
        {
            if (tableau.Length <= 1)
            {
                return tableau;
            }
            // On divise le tableau en deux
            int milieu = tableau.Length / 2;
            string[] gauche = new string[milieu];
            string[] droite = new string[tableau.Length - milieu];

            // On remplit les deux tableaux avec les mots du tableau initial
            for (int j = 0; j < milieu; j++)
            {
                gauche[j] = tableau[j];
            }

            for (int j = milieu; j < tableau.Length; j++)
            {
                droite[j - milieu] = tableau[j];
            }

            // On separer les deux tableaux en deux jusqu'a ce qu'il ne reste plus qu'un mot
            gauche = Tri_Fusion(gauche);
            droite = Tri_Fusion(droite);
            return Fusion(tableau,gauche, droite);
        }

        /// <summary> Méthode qui fusionne deux tableaux de mots </summary>
        /// <param name="tableau"> Tableau de mot qui commence par la meme lettre</param>
        /// <param name="gauche"> Moitié gauche du tableau </param>
        /// <param name="droite"> Moitié Droite du tableau </param>
        /// <returns> Tableau fusionné </returns>
        public string[] Fusion(string[]tableau, string[] gauche, string[] droite)
        {
            // On compare les mots des deux tableaux et on les ajoute dans le tableau initial
            int indexGauche = 0;
            int indexDroite = 0;
            int indexTableau = 0;

            while (indexGauche < gauche.Length && indexDroite < droite.Length)
            {
                // Si le mot de gauche est plus petit que le mot de droite, on ajoute le mot de gauche dans le tableau
                if (gauche[indexGauche].CompareTo(droite[indexDroite]) < 0)
                {
                    tableau[indexTableau] = gauche[indexGauche];
                    indexGauche++;
                }
                // Sinon on ajoute le mot de droite dans le tableau
                else
                {
                    tableau[indexTableau] = droite[indexDroite];
                    indexDroite++;
                }
                indexTableau++;
            }

            // On ajoute les mots restants dans le tableau
            while (indexGauche < gauche.Length)
            {
                tableau[indexTableau] = gauche[indexGauche];
                indexGauche++;
                indexTableau++;
            }

            // On ajoute les mots restants dans le tableau
            while (indexDroite < droite.Length)
            {
                tableau[indexTableau] = droite[indexDroite];
                indexDroite++;
                indexTableau++;
            }

            return tableau;
        }

        /// <summary> Méthode qui affiche le dictionnaire </summary>
        /// <returns> Le dictionnaire </returns>
        public string toString()
        {
            // On affiche la langue du dictionnaire
            string strLangue = "La langue du dictionnaire est " + langue + ".";
            string strNombreDeMots = "";
            
            // On affiche le nombre de mots par lettre
            for (int i = 65 ; i < 91 ; i++)
            {
                strNombreDeMots += "\n" + Convert.ToChar(i) + " : " + listeMots[i-65].Length + " mots";
            }
            return strLangue + strNombreDeMots;
        }
        #endregion
    }
}