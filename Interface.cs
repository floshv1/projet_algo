using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Interface
    {
        static ConsoleKeyInfo key;


        public static void MainMenu()
        {
        string[] options = { "Jouer à partir d'un fichier", "Jouer à partir d'un plateau généré aléatoirement","Règle du Jeu","Sortir" };

        int choix = Menu("Menu Principal", options);
        switch(choix)
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
                        AfficherScore("Fin Du Jeu",session);
                        CenterText("Appuyez sur Entrée pour aller au menu.");
                    }while(Console.ReadKey(true).Key != ConsoleKey.Enter);
                    
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
        public static void Affichage()
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                Console.ResetColor();
                Console.ForegroundColor = RandomColor();
                Console.BackgroundColor = RandomColor();
                Console.Clear();
                AfficheTitre("Titre.txt");
                Thread.Sleep(200);
            }
            CenterText("Appuyez sur Entrée");
            
            do
            {
                AfficheTitre("Titre.txt");
                CenterText("Appuyez sur Entrée");               
            }while(Console.ReadKey(true).Key != ConsoleKey.Enter);
            Console.ResetColor();
            
        }   
        public static void Charger()
        {
            Console.Clear();
            string nomDossier = "Save";
            string[] fichiers = null;
            string[] options = null;
            int i = 0;
            if (Directory.Exists(nomDossier))
            {
                fichiers = Directory.GetFiles(nomDossier);
                options = new string[fichiers.Length+1];
                foreach (string fichier in fichiers)
                {   
                    options[i] = Path.GetFileName(fichier);
                    i++;
                }
                options[i] = "Retour";

                int Choix = Menu("Choisir une sauvegarde", options);

                if (Choix< options.Length-1)
                {
                    Jeu session = new Jeu(fichiers[Choix]);
                    Jeu.BoucleJeu(session);
                    if (session.FinDuJeu())
                    {                        
                        do
                        {
                            AfficherScore("Fin Du Jeu",session);
                            CenterText("Appuyez sur Entrée pour aller au menu.");
                        }while(Console.ReadKey(true).Key != ConsoleKey.Enter);
                    
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
        public static void regle()
        {
            do
            { 
                Console.Clear();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - 9);
                WriteLineWithUnderline("Règle du Jeu");
                CenterText("- Vous aurez un temps limité pour trouver un mot dans le plateau (30, 45 ou 60 secondes).         ");
                CenterText("- Vous devrez écrire entièrement le mot et appuyer sur la touche Entrée.                          \n");
                CenterText("- Si le mot est invalide, vous ne gagnerez pas de points et vous pourrez réessayer.               ");
                CenterText("- Si le mot est valide, il sera ajouté à votre liste de mots trouvés et vous gagnerez des points. ");
                CenterText("- Vos points sont attriubué en fonction de la rareté de la lettre :                               ");
                CenterText("    - 1 point pour les lettres : E, A, I, N, O, R, S, T, U, L;                                    ");
                CenterText("    - 2 points pour les lettres : C, D, M, P;                                                     ");
                CenterText("    - 4 points pour les lettres : B, G;                                                           ");
                CenterText("    - 5 points pour les lettres : F, H, J, K, Q, V, W, X, Y, Z;                                   \n");
                CenterText("- Vous pouvez quitter la partie ou passer votre tour à tout moment en saisissant le caractère '!'.");
                CenterText("- Cependant si vous passer votre tour, vous perderez 2 points.                                    ");
                CenterText("- La partie se terminera lorsque le plateau sera vide ou lorsque le temps sera écoulé.            ");
                Console.WriteLine();
                CenterText("Appuyez sur Entrée pour aller au menu.");
            }while (Console.ReadKey(true).Key != ConsoleKey.Enter);

            Console.Clear();
            MainMenu();
            
            
        }
        public static void Sortir()
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                Console.ResetColor();
                Console.ForegroundColor = RandomColor();
                Console.BackgroundColor = RandomColor();
                Console.Clear();
                AfficheTitre("TitreFin.txt");
                Thread.Sleep(200);
            }
            CenterText("Appuyez sur Entrée pour sortir.");
            
            do
            {
                AfficheTitre("TitreFin.txt");
                CenterText("Appuyez sur Entrée pour sortir.");               
            }while(Console.ReadKey(true).Key != ConsoleKey.Enter);
            Console.ResetColor();
            Console.Clear();
            Environment.Exit(0);
        }   
        public static void AfficherScore(string txt, Jeu session)
        {
            string strMotJ1 = "";
            string strMotJ2 = "";
            Console.Clear();
            Console.SetCursorPosition(0, Console.WindowHeight / 2 - 9);
            Interface.WriteLineWithUnderline(txt);

            Interface.CenterText($"Score de {session.joueur1.Nom} : {session.joueur1.ScoresPlateau}");
            Interface.CenterText($"Mots trouvés : {session.joueur1.MotsTrouves.Count}");
            foreach(string mot in session.joueur1.MotsTrouves)
            {
                strMotJ1 += mot + " ";
            }
            Interface.CenterText(strMotJ1);
            Console.WriteLine();

            Interface.CenterText($"Score de {session.joueur2.Nom} : {session.joueur2.ScoresPlateau}");
            Interface.CenterText($"Mots trouvés : {session.joueur2.MotsTrouves.Count}");
            foreach(string mot in session.joueur2.MotsTrouves)
            {
                strMotJ2 += mot + " ";
            }
            Interface.CenterText(strMotJ2);
            Console.WriteLine();
        }
        public static ConsoleColor RandomColor()
        {
            ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
            return colors[new Random().Next(colors.Length)];
        }
        public static void WriteLineWithUnderline(string text)
        {
            CenterText(text);
            CenterText(new string('-', text.Length+2));
            Console.WriteLine();
        }
        public static void WriteWithHighlight(string text)
        {
            Console.Write("{0,"+((Console.WindowWidth / 2) - (text.Length / 2)) + "}","");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
            Console.ResetColor();
            Console.WriteLine();
        }
        public static void CenterText(string text)
        {
            Console.Write("{0,"+((Console.WindowWidth / 2) - (text.Length / 2)) + "}","");
            Console.WriteLine(text);
        }
        public static int Menu(string message, string[]options )
        {
            int selectedOptionIndex = 0;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - (options.Length) - 3);
                WriteLineWithUnderline(message);

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedOptionIndex)
                    {
                        WriteWithHighlight(" >> " + options[i] + " << ");
                    }
                    else
                    {
                        CenterText("    " + options[i] + "    ");
                    }
                }

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedOptionIndex++;
                    if (selectedOptionIndex == options.Length)
                    {
                        selectedOptionIndex = 0;
                    }
                }
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
        public static void AfficheTitre(string path)
        {
            int i = 0;
            string[] specialText = File.ReadAllLines(path);
            for (i = 0; i < specialText.Length; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - specialText[i].Length)/2, (Console.WindowHeight - specialText.Length)/2 + i);
                Console.WriteLine(specialText[i]);
            }

            Console.WriteLine("\n");
        }
        public static void AffichePlateau(char[,] matrice)
        {
            Console.WriteLine();
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                Console.Write("{0,"+((Console.WindowWidth / 2) - (matrice.GetLength(1))) + "}","");
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