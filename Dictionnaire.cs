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
                mot= mot.ToUpper();
                for (int i = 0; i < listeMots.Count; i++)
                {
                    if (mot[0] == listeMots[i][0][0])
                    {
                    lettreTrouve = true;
                    index = i;
                    }
                }   
            }
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
            if (fin < debut){
                return false;
            }
            int milieu = (debut + fin) / 2;

            if (mot == tabMots[milieu])
            {
                return true;
            }
            else if (tabMots[milieu].CompareTo(mot) > 0)
            {
                fin = milieu - 1;
            }
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

            int milieu = tableau.Length / 2;
            string[] gauche = new string[milieu];
            string[] droite = new string[tableau.Length - milieu];

            for (int j = 0; j < milieu; j++)
            {
                gauche[j] = tableau[j];
            }

            for (int j = milieu; j < tableau.Length; j++)
            {
                droite[j - milieu] = tableau[j];
            }

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
            int indexGauche = 0;
            int indexDroite = 0;
            int indexTableau = 0;

            while (indexGauche < gauche.Length && indexDroite < droite.Length)
            {
                if (gauche[indexGauche].CompareTo(droite[indexDroite]) < 0)
                {
                    tableau[indexTableau] = gauche[indexGauche];
                    indexGauche++;
                }
                else
                {
                    tableau[indexTableau] = droite[indexDroite];
                    indexDroite++;
                }
                indexTableau++;
            }

            while (indexGauche < gauche.Length)
            {
                tableau[indexTableau] = gauche[indexGauche];
                indexGauche++;
                indexTableau++;
            }

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
            string strLangue = "La langue du dictionnaire est " + langue + ".";
            string strNombreDeMots = "";
            
            for (int i = 65 ; i < 91 ; i++)
            {
                strNombreDeMots += "\n" + Convert.ToChar(i) + " : " + listeMots[i-65].Length + " mots";
            }
            return strLangue + strNombreDeMots;
        }
        #endregion
    }
}