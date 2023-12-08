using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Interface
    {
        static ConsoleKeyInfo key;

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
            CenterText("Press Enter to get to the menu.");
            Console.ResetColor();
            Console.ReadLine();
        }   

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
                    Console.Clear();
                    CenterText("Fin du jeu");
                    session.AfficherScore();
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
                int Choix = Menu("Choisir une sauvegarde : ", options);
                if (Choix< options.Length-1)
                {
                    Jeu session = new Jeu(fichiers[Choix]);
                    Jeu.BoucleJeu(session);
                    if (session.FinDuJeu())
                    {
                        Console.Clear();
                        CenterText("Fin du jeu");
                        session.AfficherScore();
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
            Console.Clear();
            CenterText("Règle du Jeu :");
            CenterText(" - Vous devrez écrire entièrement le mot et appuyer sur la touche Entrée.");
            CenterText("-Vos points sont attriubué en fonction de la rareté de la lettre");
            Console.WriteLine();
            CenterText("Press Enter to get to the menu.");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu();
                }
        }
        public static void Sortir()
        {
            Console.Clear();
            CenterText("À Bientôt");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
        public static ConsoleColor RandomColor()
        {
            ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
            return colors[new Random().Next(colors.Length)];
        }
        public static void WriteLineWithUnderline(string text, char underlineChar)
        {
            CenterText(text);
            CenterText(new string(underlineChar, text.Length+2));
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
                WriteLineWithUnderline(message, '-');
                Console.WriteLine();

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