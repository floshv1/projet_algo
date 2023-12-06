using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Interface
{
        static ConsoleKeyInfo key;

        public static void Affichage()
        {
            Console.Clear();
            string result2 = AgrandirTexte("WELCOME TO WORDSLIDE QUEST", 2);

            for (int i = 0; i < 8; i++)
            {
                WriteCentered(result2, RandomColor(), RandomColor(), true, 2);
                Thread.Sleep(400);
            }
            Console.WriteLine("\n\nPress Enter to get to the menu.");
            Console.ReadLine();
        }   

        public static void MainMenu()
        {
        string[] options = { "Jouer à partir d'un fichier", "Jouer à partir d'un plateau généré aléatoirement","Règle du Jeu","Sortir" };
        int selectedOptionIndex = 0;
        do
        {
            Console.Clear();
            WriteLineWithUnderline(" Veuillez sélectionner une option  ", '-');
            Console.WriteLine();

            int menuVerticalPosition = Console.WindowHeight / 2 - options.Length / 2;

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - options[i].Length) / 2, menuVerticalPosition + i);

                if (i == selectedOptionIndex)
                {
                    WriteWithHighlight(" " + AgrandirMotif("►", 5) + options[i]);
                }
                else
                {
                    Console.WriteLine("  " + options[i]);
                }
            }

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.DownArrow)
            {
                selectedOptionIndex = (selectedOptionIndex + 1) % options.Length;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                selectedOptionIndex = (selectedOptionIndex - 1 + options.Length) % options.Length;
            }

        } while (key.Key != ConsoleKey.Enter);

        if (selectedOptionIndex == 0)
        {
            ShowFileMenu();
        }
        if(selectedOptionIndex == 2)
        {
            Console.Clear();
            string regle  = " Règle du Jeu :"+"\n - Vous devrez écrire entièrement le mot et appuyer sur la touche Entrée."+"\n-Vos points sont attriubué en fonction de la rareté de la lettre";
            Console.Write(regle);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\n\nPress Enter to get to the menu.");
            Console.ReadLine();
            MainMenu();


        }
        if(selectedOptionIndex == 3)
        {
            Console.WriteLine("À Bientôt");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
        // Ajoutez le code pour les autres options ici

    }
        static void ShowFileMenu()
        {
            Console.Clear();
            string saveFolderPath = "save"; // Remplacez par le chemin réel de votre dossier "save"
            string[] files = Directory.GetFiles(saveFolderPath).Select(Path.GetFileName).ToArray();

            string[] fileOptions = files.Concat(new[] { "Retour" }).ToArray();
            int selectedFileIndex = 0;

            do
            {
                Console.Clear();
                WriteLineWithUnderline("  Choisissez un fichier ", '-');
                Console.WriteLine();

                int fileMenuVerticalPosition = Console.WindowHeight / 2 - fileOptions.Length / 2;

                for (int i = 0; i < fileOptions.Length; i++)
                {
                    Console.SetCursorPosition((Console.WindowWidth - fileOptions[i].Length) / 2, fileMenuVerticalPosition + i);

                    if (i == selectedFileIndex)
                    {
                        WriteWithHighlight(" " + AgrandirMotif("►", 5) + fileOptions[i]);
                            }
                    else
                 {
                     Console.WriteLine("  " + fileOptions[i]);
                 }
                }  

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedFileIndex = (selectedFileIndex + 1) % fileOptions.Length;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedFileIndex = (selectedFileIndex - 1 + fileOptions.Length) % fileOptions.Length;
                }

            } while (key.Key != ConsoleKey.Enter);

            if (selectedFileIndex < files.Length)
            {
                string selectedFile = Path.Combine(saveFolderPath, files[selectedFileIndex]);
                Console.WriteLine($"Vous avez sélectionné le fichier : {selectedFile}");
                // Ajoutez le code pour charger et jouer avec le fichier sélectionné
            }
                else if (selectedFileIndex == files.Length) // L'utilisateur a choisi "Retour"
            {
                // Retour au premier menu
                MainMenu();
            }
        }
        public static ConsoleColor RandomColor()
        {
            ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
            return colors[new Random().Next(colors.Length)];
        }

        public static void WriteLineWithUnderline(string text, char underlineChar)
        {
            CenterText(text);
            CenterText(new string(underlineChar, text.Length - 1));
            Console.WriteLine();
        }

        public static void WriteWithHighlight(string text)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteCentered(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor, bool isBold, int enlargementFactor = 100)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.WindowHeight / 2);
            Console.WriteLine(isBold ? AgrandirMotif(text.ToUpper(), enlargementFactor) : AgrandirMotif(text, enlargementFactor));
            Console.ResetColor();
        }

        public static string AgrandirMotif(string motif, int repetition)
        {
            return motif.PadRight(motif.Length * repetition, ' ');
        }

        public static string AgrandirTexte(string texte, int facteurAgrandissement)
        {
            if (facteurAgrandissement <= 0)
            {
                throw new ArgumentException("Le facteur d'agrandissement doit être supérieur à zéro.", nameof(facteurAgrandissement));
            }

            string texteAgrandi = string.Join(" ", texte.ToCharArray());

            for (int i = 1; i < facteurAgrandissement; i++)
            {
                texteAgrandi = string.Join(" ", texteAgrandi.ToCharArray());
            }

            return texteAgrandi;
        }

        public static void CenterText(string text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }
    }
}