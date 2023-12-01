using System.Linq.Expressions;
using System ; 

namespace projet_algo
{
    internal class Program
    {
        static void Main(string [] args)
        {
            Joueur player1 = new Joueur ("reda");
            player1.AddMot("salut");
            player1.AddScore("plateau1", 10);
            Console.WriteLine(player1.toString());   
        }
    }
}