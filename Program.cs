using System.Linq.Expressions;
using System;

namespace projet_algo // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        
        static void Main(string[] args)
        {
            Plateau plateauFichier = new Plateau("Test1.csv");
            Console.WriteLine(plateauFichier.toString());

            Console.WriteLine();

            Plateau plateauAleatoire = new Plateau(8,6);
            Console.WriteLine(plateauAleatoire.toString());
            
        }
    }
}