using System;

namespace projet_algo // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Plateau plateau = new Plateau("Test1.csv");

            for(int i = 0; i < plateau.Matrice.GetLength(0); i++)
            {
                for(int j = 0; j < plateau.Matrice.GetLength(1); j++)
                {
                    Console.Write(plateau.Matrice[i, j] + " ");
                }
                Console.WriteLine();
            }
            
        }
    }
}