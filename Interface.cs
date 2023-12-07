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
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            AfficheTitre("Titre.txt");
            Console.WriteLine("\n\nPress Enter to get to the menu.");
            Console.ReadLine();
        }   

        public static void MainMenu()
        {
        string[] options = { "Jouer à partir d'un fichier", "Jouer à partir d'un plateau généré aléatoirement","Règle du Jeu","Sortir" };

        int choix = Menu("Menu Principal", options);
        switch(choix)
        {
            case 0:
                break;
            case 1:
 
                break;
            case 2:
                Console.Clear();
                string regle  = " Règle du Jeu :"+"\n - Vous devrez écrire entièrement le mot et appuyer sur la touche Entrée."+"\n-Vos points sont attriubué en fonction de la rareté de la lettre";
                Console.Write(regle);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\n\nPress Enter to get to the menu.");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu();
                }
                break;
            case 3:
                Sortir();
                break;
        }
    }
        public static void Sortir()
        {
            Console.WriteLine("À Bientôt");
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
    }
}