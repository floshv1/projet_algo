using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public static void BoucleJeu(Jeu session)
        {
            if(session.joueur1.EnJeu == true)
            {
                Thread.Sleep(1000);
                session.Jouer(session.joueur1);
                if(Program.jump != Program.Jump.Continue) return;
                AuTourDe(session,"joueur2");
                session.SaveGameToCSV($"Save/save{session.nom}.csv");
                if (session.plateau.estVide() == true)
                {
                    Console.WriteLine("Le plateau est vide");
                    session.joueur1.EnJeu = false;
                    session.joueur2.EnJeu = false;
                }else BoucleJeu(session);
            }
            else if(session.joueur2.EnJeu == true)
            {
                Thread.Sleep(1000);
                session.Jouer(session.joueur2);
                if(Program.jump != Program.Jump.Continue) return;
                AuTourDe(session,"joueur1");
                session.SaveGameToCSV($"Save/save{session.nom}.csv");

                if (session.plateau.estVide() == true)
                {
                    Console.WriteLine("Le plateau est vide");
                    session.joueur1.EnJeu = false;
                    session.joueur2.EnJeu = false;
                }else BoucleJeu(session);
            }
            else
            {
                Thread.Sleep(1000);
                AuTourDe(session,"joueur1");
                session.SaveGameToCSV($"Save/save{session.nom}.csv");
                session.Jouer(session.joueur1);
                if(Program.jump != Program.Jump.Continue) return;
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
            Console.Clear();
            dico.Tri_Fusion_Dico();
            Interface.CenterText($"C'est au tour de {joueur.Nom} de jouer.");
            Interface.CenterText("Tu as 45 secondes pour touver un mot pour chercher un mot.");
            do
            {
                Console.Write("{0,"+((Console.WindowWidth / 2) - ("Commence dans : ".Length / 2)) + "}","");
                Console.Write($"Commence dans : {cpt}");
                Thread.Sleep(1000);
                Console.Write("\r");
                cpt--;
            } while(cpt > 0);
            DateTime debut = DateTime.Now;
            TimeSpan duree = TimeSpan.FromSeconds(45);
            DateTime fin = debut + duree;
            do
            {
                Interface.CenterText($"{fin - DateTime.Now}");
                Interface.AffichePlateau(plateau.Matrice);
                Console.Write("\n");
                Console.Write("{0,"+((Console.WindowWidth / 2) - ("Entrez un mot : ".Length / 2)) + "}","");
                Console.Write("Entrez un mot : ");
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
                                    Interface.CenterText("Le mot n'est pas dans le plateau");
                                }
                                else
                                {
                                    verif = true;
                                    joueur.AddMot(mot);
                                    joueur.AddScore(mot.Length);
                                    plateau.GlisserLettres();
                                    Interface.CenterText("Le mot a été trouvé");
                                }
                            }
                            else Interface.CenterText("Le mot a déjà été trouvé");
                        }
                        else Interface.CenterText("Le mot n'est pas dans le dictionnaire");
                    }
                    else
                    {
                        switch(Interface.Menu("MENU", new string[] { "Reprendre","Passer son tour", "Quitter la partie"})){
                            case 0:
                                break;
                            case 1:
                                Interface.CenterText("Vous avez passé votre tour");
                                verif = true;
                                break;
                            case 2:
                                Interface.CenterText("Vous avez quitté la partie");
                                Program.jump = Program.Jump.Main_Menu;
                                verif = true;
                                break;
                        }
                    }
                }
                else 
                {
                    Interface.CenterText("Le mot est vide");
                }
                Console.WriteLine();
                
            }while(DateTime.Now < fin && verif == false);
        }
        public static string SaisieJoueur(string message)
        {
            string nomJoueur = "";
            do
            {
                Interface.CenterText(message);
                Console.Write("{0,"+((Console.WindowWidth / 2) - (message.Length / 2)) + "}","");
                Console.Write("> ");
                nomJoueur = Console.ReadLine();
                Thread.Sleep(1000);
                Console.Clear();
            } while (nomJoueur == "");
            return nomJoueur;
        }
        public static string SaisiePlateau(string message)
        {
            string nomPlateau = "";
            do
            {
                Interface.CenterText(message);
                Console.Write("{0,"+((Console.WindowWidth / 2) - (message.Length / 2)) + "}","");
                Console.Write("> ");
                nomPlateau = Console.ReadLine();
                Thread.Sleep(1000);
                Console.Clear();
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
        public void AfficherScore()
        {
            Console.WriteLine("Score de {0} : {1}", joueur1.Nom, joueur1.ScoresPlateau);
            Console.WriteLine("Score de {0} : {1}", joueur2.Nom, joueur2.ScoresPlateau);
        }
        public bool FinDuJeu()
        {
            bool fin = false;
            if (joueur1.EnJeu == false && joueur2.EnJeu == false)
            {
                fin = true;
            }
            
            return fin;
        }
    }
}