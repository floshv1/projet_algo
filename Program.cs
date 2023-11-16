using System.Linq.Expressions;
using System;

namespace projet_algo // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static void AfficheMatrice(char[,] matrice)
        {
            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for(int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write(matrice[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Plateau plateauFichier = new Plateau("Test1.csv");
            AfficheMatrice(plateauFichier.Matrice);

            Console.WriteLine();
            Plateau plateauAleatoire = new Plateau(8);
            AfficheMatrice(plateauAleatoire.Matrice);
            
        }
    }
}