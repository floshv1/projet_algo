using System.Security.Permissions;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Jeu
    {
        #region Attributs
        /// <summary> Nom de la partie </summary>
        string nom;

        /// <summary> Joueur 1 </summary>
        public Joueur joueur1;

        /// <summary> Joueur 2 </summary>
        public Joueur joueur2;

        /// <summary> Plateau de jeu </summary>
        Plateau plateau;

        /// <summary> Dictionnaire </summary>
        Dictionnaire dico;

        /// <summary> Temps d'un tour </summary>
        int temps;
        #endregion
        
        #region Propriétés

        /// <summary> Propriété du nom de la partie </summary>
        public string Nom
        {
            get { return nom; }
        }

        /// <summary> Propriété du joueur 1 </summary>
        public Joueur Joueur1
        {
            get { return joueur1; }
        }

        /// <summary> Propriété du joueur 2 </summary>
        public Joueur Joueur2
        {
            get { return joueur2; }
        }

        /// <summary> Propriété du plateau de jeu </summary>
        public Plateau Plateau
        {
            get { return plateau; }
        }

        /// <summary> Propriété du dictionnaire </summary>
        public Dictionnaire Dico
        {
            get { return dico; }
        }

        /// <summary> Propriété du temps d'un tour </summary>
        public int Temps
        {
            get { return temps; }
        }
        #endregion

        #region Constructeurs

        /// <summary> Constructeur du Jeu apr defaut </summary>
        /// <returns> Un nouveau jeu </returns>
        public Jeu()
        {
            this.nom = SaisiePlateau("Entrez le nom de la Partie : ");
            this.joueur1 = new Joueur(SaisieJoueur("Entrez le nom du joueur 1 : "));
            joueur1.EnJeu = true;
            this.joueur2 = new Joueur(SaisieJoueur("Entrez le nom du joueur 2 : "));
            DefinirTaille();
            DefinirLangue();
            DefinirTemps();
            SaveGameToCSV($"Save/save{nom}.csv");
        }

        /// <summary> Constructeur du Jeu à partir d'un fichier </summary>
        /// <param name="filename"> Nom du fichier </param>
        /// <returns> Un nouveau jeu </returns>
        public Jeu(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);
                this.nom = lines[0];
                this.joueur1 = Joueur.StringToJoueur(lines[1]);
                this.joueur2 = Joueur.StringToJoueur(lines[2]);
                this.dico = new Dictionnaire(lines[3]);
                this.temps = int.Parse(lines[4]);
                plateau = new Plateau();

                plateau.Matrice = new char[lines.Length - 5, lines[5].Split(';').Length];
                for(int i = 5; i < lines.Length; i++)
                {
                    string[] lineSplit = lines[i].Split(';');
                    for(int j = 0; j < lineSplit.Length; j++)
                    {
                        plateau.Matrice[i-5,j] = lineSplit[j][0];
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.SetCursorPosition(0, Console.WindowHeight / 2);
                Interface.CenterText("Mauvais format de fichier");
                Thread.Sleep(2000);
                Interface.MainMenu();
            }
        }
        #endregion
        
        #region Méthodes

        /// <summary> Méthode qui lance le jeu </summary>
        /// <param name="session"> Session de jeu </param>
        /// <returns> Si le jeu est fini </returns>
        public static bool BoucleJeu(Jeu session)
        {
            while(session.FinDuJeu() == false)
            {
                if(session.joueur1.EnJeu == true)
                {
                    Thread.Sleep(2000);
                    session.Jouer(session.joueur1);
                    AuTourDe(session,"joueur2");
                    session.SaveGameToCSV($"Save/save{session.nom}.csv");
                    BoucleJeu(session);
                }
                else if(session.joueur2.EnJeu == true)
                {
                    Thread.Sleep(2000);
                    session.Jouer(session.joueur2);
                    AuTourDe(session,"joueur1");
                    session.SaveGameToCSV($"Save/save{session.nom}.csv");
                    Thread.Sleep(500);
                    Interface.AfficherScore("Score",session);
                    Thread.Sleep(2000);
                    BoucleJeu(session);
                }
            }

            return true;
        }
        
        /// <summary> Méthode qui défini le joueur qui doit jouer </summary>
        /// <param name="session"> Session de jeu </param>
        /// <param name="nomJoueur"> Nom du joueur </param>
        /// <returns> Le joueur qui doit jouer </returns>
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
        
        /// <summary> Definie la langue du dictionnaire </summary>
        /// <returns> La langue du dictionnaire </returns>
        public void DefinirLangue()
        {
            DefinirLangue :
            Console.Clear();
            switch(Interface.Menu("Choix de la langue", new string[] { "Français", "Anglais" }))
            {
                case 0:
                    dico = new Dictionnaire("Français");
                    break;
                case 1:
                    Console.Clear();
                    Console.SetCursorPosition(0, Console.WindowHeight / 2);
                    dico = new Dictionnaire("Anglais");
                    Interface.CenterText("La langue Anglaise n'est pas encore disponible");
                    Thread.Sleep(3000);
                    goto DefinirLangue;
            }
        }
        
        /// <summary> Definie la taille du plateau </summary>
        /// <returns> La taille du plateau </returns>
        public void DefinirTaille()
        {
            Console.Clear();
            switch(Interface.Menu("Taille du plateau", new string[] {"6 x 6","8 x 8", "10 x 10" }))
            {
                case 0:
                    plateau = new Plateau(6,6);
                    break;
                case 1:
                    plateau = new Plateau(8,8);
                    break;
                case 2:
                    plateau = new Plateau(10,10);
                    break;
            }
        }

        /// <summary> Definie le temps d'un tour </summary>
        /// <returns> Le temps d'un tour </returns>
        public void DefinirTemps()
        {
            Console.Clear();
            switch(Interface.Menu("Temps d'un tour", new string[] {"30 secondes","45 secondes", "60 secondes" }))
            {
                case 0:
                    temps = 30;
                    break;
                case 1:
                    temps = 45;
                    break;
                case 2:
                    temps = 60;
                    break;
            }
        }
        
        /// <summary> Méthode qui verifie si le mot entré est bien dans le plateau</summary>
        /// <param name="joueur"> Joueur qui joue </param>
        /// <returns> le tour d'un joueur </returns>
        public void Jouer(Joueur joueur)
        {
            int cpt = 5;
            string mot = "";
            bool verif = false;
            bool finTemps = false;
            joueur.Skip = false;

            Console.Clear();
            Console.SetCursorPosition(0, Console.WindowHeight / 2 - 3);
            dico.Tri_Fusion_Dico();

            Interface.CenterText($"C'est au tour de {joueur.Nom} de jouer.");
            Interface.CenterText($"Tu as {temps} secondes pour touver un mot pour chercher un mot.");

            do
            {
                Console.Write("{0,"+((Console.WindowWidth / 2) - ("Commence dans : ".Length / 2)) + "}","");
                Console.Write($"Commence dans : {cpt}");
                Thread.Sleep(1000);
                Console.Write("\r");
                cpt--;
            } while(cpt > 0);

            Console.Clear();

            DateTime debut = DateTime.Now;
            TimeSpan duree = TimeSpan.FromSeconds(temps);
            DateTime fin = debut + duree;

            do
            {
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - plateau.Matrice.GetLength(0) / 2 - 3);
                Console.WriteLine();
                Interface.CenterText($"C'est au tour de {joueur.Nom} de jouer.");
                Interface.CenterText($"{fin-DateTime.Now}");
                Interface.AffichePlateau(plateau.Matrice);

                Console.Write("\n");
                Console.Write("{0,"+((Console.WindowWidth / 2) - ("Entrez un mot : ".Length / 2)) + "}","");
                Console.Write("Entrez un mot : ");
                mot = Console.ReadLine();

                Console.Clear();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - plateau.Matrice.GetLength(0) / 2 - 4);
                if (DateTime.Now > fin)
                {
                    finTemps = true;
                }
                else
                {
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
                                            joueur.AddScore(mot);
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
                            switch(Interface.Menu("MENU", new string[] { "Reprendre","Voir Score","Passer son tour", "Quitter la partie"})){
                                case 0:
                                    Console.Clear();
                                    break;
                                case 1:
                                    do
                                    {
                                        Interface.AfficherScore("Score",this );
                                        Interface.CenterText("Retour au jeu");

                                    }while(Console.ReadKey(true).Key != ConsoleKey.Enter);

                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.SetCursorPosition(0, Console.WindowHeight / 2 );
                                    Interface.CenterText("Vous avez passé votre tour");
                                    joueur.Skip = true;
                                    joueur.ScoresPlateau -= 2;
                                    verif = true;
                                    break;
                                case 3:
                                    Console.Clear();
                                    Console.SetCursorPosition(0, Console.WindowHeight / 2 );
                                    Interface.CenterText("Vous avez quitté la partie");
                                    Thread.Sleep(2000);
                                    Interface.MainMenu();
                                    verif = true;
                                    break;
                            }
                        }
                    }
                    else 
                    {
                        Interface.CenterText("Le mot est vide ");
                    }
                }
                Console.WriteLine();
                
            }while(finTemps == false && verif == false);

            if(verif == false)
            {
                Interface.CenterText("Temps écoulé");
                joueur.Skip = true;
                joueur.ScoresPlateau -= 2;
            }

            Interface.CenterText("Fin du tour");
        }
        
        /// <summary> Méthode qui demande le nom du joueur </summary>
        /// <param name="message"> Message à afficher </param>
        /// <returns> Le nom du joueur </returns>
        public string SaisieJoueur(string message)
        {
            string nomJoueur = "";
            do
            {
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - 4);
                Interface.EcritSouligner(message);
                Console.Write("{0,"+((Console.WindowWidth / 2) - (message.Length / 2)) + "}","");
                Console.Write("> ");
                nomJoueur = Console.ReadLine();
                Thread.Sleep(200);
                Console.Clear();
            } while (nomJoueur == "");
            return nomJoueur;
        }

        /// <summary> Méthode qui demande le nom de la partie </summary>
        /// <param name="message"> Message à afficher </param>
        /// <returns> Le nom de la partie </returns>
        public string SaisiePlateau(string message)
        {
            string nomPlateau = "";          
            do
            {
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - 4);
                Interface.EcritSouligner(message);
                Console.Write("{0,"+((Console.WindowWidth / 2) - (message.Length / 2)) + "}","");
                Console.Write("> ");
                nomPlateau = Console.ReadLine();
                Thread.Sleep(200);
                Console.Clear();
            } while (nomPlateau == "");
            return nomPlateau;
        }
        
        /// <summary> Méthode qui sauvegarde la partie dans un fichier </summary>
        /// <param name="nomFichier"> Nom du fichier </param>
        /// <returns> La partie sauvegardée </returns>
        public void SaveGameToCSV(string nomFichier)
        {            
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(nom);
            sw.WriteLine(joueur1.toFile());
            sw.WriteLine(joueur2.toFile());
            sw.WriteLine(dico.Langue);
            sw.WriteLine(temps);
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
        
        /// <summary> Méthode qui vérifie si la partie est finie </summary>
        /// <returns> Si la partie est finie </returns>
        public bool FinDuJeu()
        {
            bool fin = false;
            if (plateau.estVide() == true || (joueur1.Skip && joueur2.Skip))
            {
                fin = true;
            }
            
            return fin;
        }
        #endregion
    }
}