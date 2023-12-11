using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Threading;


namespace projet_algo
{
    public class Interface
    {
        /// <summary> Variable qui permet de récupérer la touche appuyée </summary>
        static ConsoleKeyInfo key;

        /// <summary> Affiche le menu principale </summary>
        /// <returns> Le menu principale </returns>
        public static void MainMenu()
        {
            string[] options = { "Jouer à partir d'un fichier", "Jouer à partir d'un plateau généré aléatoirement", "Règle du Jeu", "Sortir" };

            int choix = Menu("Menu Principal", options);
            switch (choix)
            {
                case 0:
                    Charger();
                    break;
                case 1:
                    Console.Clear();
                    Jeu session = new Jeu();

                    Jeu.BoucleJeu(session);
                    if (session.FinDuJeu())
                    {
                        do
                        {
                            AfficherScore("Fin Du Jeu", session);
                            CenterText("Appuyez sur Entrée pour aller au menu.");
                        } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

                        Console.Clear();
                        MainMenu();

                    }
                    break;
                case 2:
                    regle();
                    break;
                case 3:
                    Sortir();
                    break;
            }
        }

        /// <summary> AFfichage Personnalisé du titre </summary>
        /// <returns> Le titre </returns>
        public static void Affichage()
        {
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            AfficheTitre("Titre.txt");
            Thread.Sleep(1000);
            CenterText("Appuyez sur Entrée");

            do
            {
                Console.Clear();
                AfficheTitre("Titre.txt");
                CenterText("Appuyez sur Entrée");
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
            Console.ResetColor();

        }

        /// <summary> Affiche le menu des sauvegardes </summary>
        /// <returns> Le menu des sauvegardes </returns>
        public static void Charger()
        {
            Console.Clear();
            string nomDossier = "Save";
            string[] fichiers = null;
            string[] options = null;
            int i = 0;
            if (Directory.Exists(nomDossier))
            {
                // Stocke les noms des fichiers du dossier dans un tableau
                fichiers = Directory.GetFiles(nomDossier);

                // Initialise le tableau des options avec la taille du tableau des fichiers
                options = new string[fichiers.Length + 1];
                foreach (string fichier in fichiers)
                {
                    // Stocke les noms des fichiers dans le tableau des options
                    options[i] = Path.GetFileName(fichier);
                    i++;
                }
                // Ajoute l'option retour dans le dernier index du tableau des options
                options[i] = "Retour";

                int Choix = Menu("Choisir une sauvegarde", options);

                // Si l'option choisie est différente de l'option retour
                // On lance le jeu avec le fichier choisi
                if (Choix < options.Length - 1)
                {
                    Jeu session = new Jeu(fichiers[Choix]);
                    Jeu.BoucleJeu(session);
                    if (session.FinDuJeu())
                    {
                        do
                        {
                            AfficherScore("Fin Du Jeu", session);
                            CenterText("Appuyez sur Entrée pour aller au menu.");
                        } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

                        Console.Clear();
                        MainMenu();
                    }
                }
                else
                {
                    Console.Clear();
                    MainMenu();
                }
            }
            else
            {
                Console.WriteLine("Le dossier n'existe pas");
            }
        }

        /// <summary> Affiche les règles du jeu </summary>
        /// <returns> Les règles du jeu </returns>
        public static void regle()
        {
            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - 9);
                EcritSouligner("Règle du Jeu");
                CenterText("- Vous aurez un temps limité pour trouver un mot dans le plateau (30, 45 ou 60 secondes).         ");
                CenterText("- La recherche de mot commence a partie de la base du plateau.                                    ");
                CenterText("- Vous devrez écrire entièrement le mot et appuyer sur la touche Entrée.                          \n");
                CenterText("- Si le mot est invalide, vous ne gagnerez pas de points et vous pourrez réessayer.               ");
                CenterText("- Si le mot est valide, il sera ajouté à votre liste de mots trouvés et vous gagnerez des points. ");
                CenterText("- Vos points sont attriubué en fonction de la rareté de la lettre :                               ");
                CenterText("    - 1 point pour les lettres : E, A, I, N, O, R, S, T, U, L;                                    ");
                CenterText("    - 2 points pour les lettres : C, D, M, P;                                                     ");
                CenterText("    - 4 points pour les lettres : B, G;                                                           ");
                CenterText("    - 5 points pour les lettres : F, H, J, K, Q, V, W, X, Y, Z;                                   \n");
                CenterText("- Vous pouvez quitter la partie ou passer votre tour à tout moment en saisissant le caractère '!'.");
                CenterText("- Cependant vous pouvez perdre des points en passant votre tour ou si le temps est écoulé.        ");
                CenterText("- La partie se terminera lorsque le plateau sera vide ou si les deux joueurs passent leur tour.   ");
                Console.WriteLine();
                CenterText("Appuyez sur Entrée pour aller au menu.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

            Console.Clear();
            MainMenu();


        }

        /// <summary> Affichage personnalisé de sortie de jeu  </summary>
        /// <returns> Sortie de jeu </returns>
        public static void Sortir()
        {
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            AfficheTitre("TitreFin.txt");
            Thread.Sleep(1000);
            CenterText("Appuyez sur Entrée");

            do
            {
                Console.Clear();
                AfficheTitre("TitreFin.txt");
                CenterText("Appuyez sur Entrée");
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
            Console.Clear();
            Console.ResetColor();
            Environment.Exit(0);
        }

        /// <summary> Ecrit un texte avec un soulignage </summary>
        /// <param name="text"> le texte a souligner </param>
        /// <returns> le texte souligné </returns>
        public static void EcritSouligner(string text)
        {
            // On affiche le texte centré
            CenterText(text);
            // On affiche un soulignage en dessous du texte
            CenterText(new string('-', text.Length + 2));
            Console.WriteLine();
        }

        /// <summary> Ecrit un texte avec un surlignage </summary>
        /// <param name="text"> le texte a surligner </param>
        /// <returns> le texte surligné </returns>
        public static void EcritSurligner(string text)
        {
            // On affiche le texte centré
            Console.Write("{0," + ((Console.WindowWidth / 2) - (text.Length / 2)) + "}", "");

            // On affiche un surlignage en dessous du texte
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary> Ecrit un texte centré </summary>
        /// <param name="text"> le texte a centrer </param>
        /// <returns> le texte centré </returns>
        public static void CenterText(string text)
        {
            Console.Write("{0," + ((Console.WindowWidth / 2) - (text.Length / 2)) + "}", "");
            Console.WriteLine(text);
        }

        /// <summary> Affiche un menu bouclé </summary>
        /// <param name="message"> le message a afficher </param>
        /// <param name="options"> les options du menu </param>
        /// <returns> le menu bouclé </returns>
        public static int Menu(string message, string[] options)
        {
            // On initialise l'index de l'option sélectionnée à 0
            int selectedOptionIndex = 0;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - (options.Length)/2 - 3);
                EcritSouligner(message);

                // On affiche les options du menu
                // Si l'option est sélectionnée, on l'affiche avec un surlignage
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedOptionIndex)
                    {
                        EcritSurligner(" >> " + options[i] + " << ");
                    }
                    else
                    {
                        CenterText("    " + options[i] + "    ");
                    }
                }

                // On récupère la touche appuyée
                key = Console.ReadKey(true);

                // Si la touche appuyée est la flèche du bas, on incrémente l'index de l'option sélectionnée
                // Si l'index est égal à la taille du tableau, on le remet à 0 ce qui permet de boucler le menu
                if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedOptionIndex++;
                    if (selectedOptionIndex == options.Length)
                    {
                        selectedOptionIndex = 0;
                    }
                }
                // Si la touche appuyée est la flèche du haut, on décrémente l'index de l'option sélectionnée
                // Si l'index est inférieur à 0, on le remet à la taille du tableau - 1 ce qui permet de boucler le menu
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedOptionIndex--;
                    if (selectedOptionIndex < 0)
                    {
                        selectedOptionIndex = options.Length - 1;
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            return selectedOptionIndex;
        }

        /// <summary> Affiche le titre centré </summary>
        /// <param name="fileName"> Fichier contenant un texte special </param>
        /// <returns> Le titre centré </returns>
        public static void AfficheTitre(string fileName)
        {
            int i = 0;
            // On lit le fichier contenant le texte spécial
            string[] TexteSpecial = File.ReadAllLines(fileName);
            for (i = 0; i < TexteSpecial.Length; i++)
            {
                // On affiche le texte centré en largeur et en hauteur
                Console.SetCursorPosition((Console.WindowWidth - TexteSpecial[i].Length) / 2, (Console.WindowHeight - TexteSpecial.Length) / 2 + i);
                Console.WriteLine(TexteSpecial[i]);
            }

            Console.WriteLine("\n");
        }

        /// <summary> Affiche le score des joueurs </summary>
        /// <param name="txt"> Le texte a afficher </param>
        /// <param name="session"> La session de jeu </param>
        /// <returns> Le score des joueurs </returns>
        public static void AfficherScore(string txt, Jeu session)
        {
            string strMotJ1 = "";
            string strMotJ2 = "";
            Console.Clear();
            Console.SetCursorPosition(0, Console.WindowHeight / 2 - 5);
            Interface.EcritSouligner(txt);

            // On affiche les scores des joueurs
            // On affiche les mots trouvés par les joueurs
            // Pas fait dans la classe Joueurs car on a besoin de la session de jeu
            Interface.CenterText($"Score de {session.Joueur1.Nom} : {session.Joueur1.ScoresPlateau}");
            Interface.CenterText($"Mots trouvés : {session.Joueur1.MotsTrouves.Count}");
            foreach (string mot in session.Joueur1.MotsTrouves)
            {
                strMotJ1 += mot + " ";
            }
            Interface.CenterText(strMotJ1);
            Console.WriteLine();

            Interface.CenterText($"Score de {session.Joueur2.Nom} : {session.Joueur2.ScoresPlateau}");
            Interface.CenterText($"Mots trouvés : {session.Joueur2.MotsTrouves.Count}");
            foreach (string mot in session.Joueur2.MotsTrouves)
            {
                strMotJ2 += mot + " ";
            }
            Interface.CenterText(strMotJ2);
            Console.WriteLine();
        }

        /// <summary> Affiche le plateau centré </summary>
        /// <param name="matrice"> Le plateau </param>
        /// <returns> Le plateau centré </returns>
        public static void AffichePlateau(char[,] matrice)
        {
            Console.WriteLine();
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                Console.Write("{0," + ((Console.WindowWidth / 2) - (matrice.GetLength(1))) + "}", "");
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write(matrice[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}