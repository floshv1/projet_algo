using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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
            if (File.Exists(fileName))
            {
                ReadFile(fileName);
            }
            else
            {
                Interface.CenterText($"Le fichier {fileName} n'existe pas");
            }
        }

        public List<string[]> ListeMots 
        { 
            get{ return listeMots;}
        }
        public string Langue
        {
            get { return langue; }
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
            string strLangue = "La langue du dictionnaire est " + langue + ".";
            string strNombreDeMots = "";
            
            for (int i = 65 ; i < 91 ; i++)
            {
                strNombreDeMots += "\n" + Convert.ToChar(i) + " : " + listeMots[i-65].Length + " mots";
            }
            return strLangue + strNombreDeMots;
        }

    }
}