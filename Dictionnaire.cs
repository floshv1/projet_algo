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
        List<string[]> listeMots;

        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string fileName = "Mots_" + langue + ".txt";
            ReadFile(fileName);
        }

        public List<string[]> ListeMots 
        { 
            get{ return listeMots;}
        }

        public void ReadFile(string fileName)
        {
            listeMots = new List<string[]>();
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();

            while (line != null)
            {
                string[] words = line.Split(" ");
                listeMots.Add(words);
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

        public void Tri_Fusion_Dico()
        {
            for (int i = 0; i < listeMots.Count; i++)
            {
                Tri_Fusion(listeMots[i]);
            }
        }
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

        public string toString()
        {
            int nombreDeMots = 0;
            foreach (string[] words in listeMots)
            {
                nombreDeMots += words.Length;
            }
            string strNombreDeMots= "Il y a " + nombreDeMots + " mots dans le dictionnaire.";
            return strNombreDeMots + "\n" + "La langue du dictionnaire est " + langue + ".";
        }

    }
}