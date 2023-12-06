using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Jeu
    {
        public string nom;
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

            plateau.Matrice = new char[lines.Length - 3, lines[3].Split(';').Length];
            for(int i = 3; i < lines.Length; i++)
            {
                string[] lineSplit = lines[i].Split(';');
                for(int j = 0; j < lineSplit.Length; j++)
                {
                    plateau.Matrice[i-3,j] = lineSplit[j][0];
                }
            }
        }

        public void BoucleJeu(Jeu session)
        {
            if(session.joueur1.EnJeu == true)
            {
                session.Jouer(session.joueur1);
                AuTourDe(session,"joueur2");
                session.SaveGameToCSV($"Save/save{session.nom}.csv");

                BoucleJeu(session);
            }
            else if(session.joueur2.EnJeu == true)
            {
                session.Jouer(session.joueur2);
                AuTourDe(session,"joueur1");
                session.SaveGameToCSV($"Save/save{session.nom}.csv");

                BoucleJeu(session);
            }
            else
            {
                AuTourDe(session,"joueur1");
                session.SaveGameToCSV($"Save/save{session.nom}.csv");
                session.Jouer(session.joueur1);
                AuTourDe(session,"joueur2");
                session.SaveGameToCSV($"Save/save{session.nom}.csv");
                BoucleJeu(session);
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
            bool verif = false;

            dico.Tri_Fusion_Dico();
            Console.WriteLine("C'est au tour de {0} de jouer. \nTu as 45 secondes pour touver un mot pour chercher un mot. TOP !", joueur.Nom);
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
                Console.WriteLine(fin - DateTime.Now);
                Console.WriteLine(plateau.toString());
                Console.WriteLine();
                Console.WriteLine("Entrez un mot : ");
                mot = Console.ReadLine();
                Console.Clear();
                if(mot != null && mot != "")
                {
                    if(mot != "!")
                    {
                        if (dico.RecheDichoDico(mot) == true)
                        {
                            if (!joueur.Contient(mot))
                            {
                                if (plateau.Recherche_Mot(mot) == false)
                                {
                                    Console.WriteLine("Le mot n'est pas dans le plateau");
                                }
                                else
                                {
                                    verif = true;
                                    joueur.AddMot(mot);
                                    joueur.AddScore(mot.Length);
                                    plateau.GlisserLettres();
                                    Console.WriteLine("Le mot a été trouvé");
                                }
                            }
                            else Console.WriteLine("Le mot a déjà été trouvé");
                        }
                        else Console.WriteLine("Le mot n'est pas dans le dictionnaire");
                    }
                    else
                    {
                        Console.WriteLine("Vous avez quitté la partie");
                        joueur.EnJeu = false;
                        verif = true;
                    }
                }
                else 
                {
                    Console.WriteLine("Le mot est vide");
                }
                Console.WriteLine();
                
            }while(DateTime.Now < fin && verif == false);
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