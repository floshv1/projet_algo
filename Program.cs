using System.Linq.Expressions;
using System ; 

namespace projet_algo
{
    internal class Program
    {
        static void Main(string [] args)
        {
            Joueur player1 = new Joueur ("reda");
            string mot = "zzzz";
            player1.AddMot(mot);
            player1.AddScore(mot);
            player1.AddMot("1");
            player1.AddScore("1");
            Console.WriteLine(player1.toString());   
        }
    }
}