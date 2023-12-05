using System.Linq.Expressions;
using System;

namespace projet_algo // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Plateau plateauFichier = new Plateau("Test1.csv");
            Plateau plateauAleatoire = new Plateau(8, 8);
            Console.WriteLine(plateauFichier.toString());

            Console.WriteLine();
            Console.WriteLine("Entrez un mot à rechercher : ");
            string mot = Console.ReadLine();
            Console.Clear();

            plateauFichier.Recherche_Mot(mot);
            plateauFichier.GlisserLettres();
        }
    }
}