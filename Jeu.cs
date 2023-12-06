using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Jeu
    {
        string nom;
        public Joueur joueur1;
        public Joueur joueur2;
        Plateau plateau;
        Dictionnaire dico = new Dictionnaire("Français");

        public Jeu(int ligne, int colonne)
        {
            this.nom = SaisiePlateau("Entrez le nom de la Partie : ");
            this.joueur1 = new Joueur(SaisieJoueur("Entrez le nom du joueur 1 : "));
            this.joueur2 = new Joueur(SaisieJoueur("Entrez le nom du joueur 2 : "));
            this.plateau = new Plateau(ligne, colonne);
            SaveGameToCSV($"Save/save{nom}.csv");
        }
        public Jeu(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            this.nom = lines[0];
            this.joueur1 = Joueur.StringToJoueur(lines[1]);
            this.joueur2 = Joueur.StringToJoueur(lines[2]);
            plateau = new Plateau();
            for(int i = 3; i < lines.Length; i++)
            {
                string[] lineSplit = lines[i].Split(';');
                char[] c = new char[lineSplit.Length];
                for(int j = 0; j < lineSplit.Length; j++)
                {
                    c[j] = lineSplit[j][0];
                }
                int ligne = i - 3;
                for (int j = 0; j < plateau.Matrice.GetLength(1); j++)
                {
                    plateau.Matrice[ligne, j] = c[j];
                }
            }
        }


        public static void AuTourDe(Jeu session,string nomJoueur)
        {
            if (nomJoueur == "joueur1")
            {
                session.joueur1.EnJeu = true;
                session.joueur2.EnJeu = false;
            }
            else if (nomJoueur == "joueur2")
            {
                session.joueur1.EnJeu = false;
                session.joueur2.EnJeu = true;
            
            }
        }
        public void Jouer(Joueur joueur)
        {
            int cpt = 5;
            string mot = "";
            dico.Tri_Fusion_Dico();
            Console.WriteLine("C'est au tour de {0} de jouer. Tu as 45 secondes pour touver un mot pour chercher un mot ", joueur.Nom);
            do
            {
                Console.Write($"\r{cpt}");
                Thread.Sleep(1000);
                cpt--;
            } while(cpt > 0);
            DateTime debut = DateTime.Now;
            TimeSpan duree = TimeSpan.FromSeconds(45);
            DateTime fin = debut + duree;
            do
            {
                
                Console.WriteLine( fin - debut);
                Console.WriteLine(plateau.toString());
                Console.WriteLine();
                Console.WriteLine("Entrez un mot : ");
                mot = Console.ReadLine();
                if(mot== null || mot == "")
                {
                    Console.WriteLine("Le mot est vide");
                }
                else if (dico.RecheDichoDico(mot) == false)
                {
                    Console.WriteLine("Le mot n'est pas dans le dictionnaire");
                }
                else if (joueur.MotsTrouves.Contains(mot))
                {
                    Console.WriteLine("Le mot a déjà été trouvé");
                }
                else if (plateau.Recherche_Mot(mot) == false)
                {
                    Console.WriteLine("Le mot n'est pas dans le plateau");
                }
                else
                {
                    
                    joueur.AddScore(mot.Length);
                    Console.WriteLine("Le mot a été trouvé");
                }
            }while(DateTime.Now < fin && plateau.Recherche_Mot(mot) == false);
        }

        public static string SaisieJoueur(string message)
        {
            string nomJoueur = "";
            do
            {
                Console.WriteLine(message);
                Console.Write("{0,"+((Console.WindowWidth / 2) - (message.Length / 2)) + "}","");
                Console.Write("> ");
                nomJoueur = Console.ReadLine();
            } while (nomJoueur == "");
            return nomJoueur;
        }
        public static string SaisiePlateau(string message)
        {
            string nomPlateau = "";
            do
            {
                Console.WriteLine(message);
                Console.Write("{0,"+((Console.WindowWidth / 2) - (message.Length / 2)) + "}","");
                Console.Write("> ");
                nomPlateau = Console.ReadLine();
            } while (nomPlateau == "");
            return nomPlateau;
        }
        public void SaveGameToCSV(string nomFichier)
        {            
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(nom);
            sw.WriteLine(joueur1.toFile());
            sw.WriteLine(joueur2.toFile());
            for (int i = 0; i < plateau.Matrice.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.Matrice.GetLength(1); j++)
                {
                    if (j == plateau.Matrice.GetLength(1) - 1)
                    {
                        sw.Write(plateau.Matrice[i, j]);
                    }
                    else
                    {
                        sw.Write(plateau.Matrice[i, j] + ";");
                    }
                }
                sw.WriteLine();
            }
            sw.Close();
        }
    }
}