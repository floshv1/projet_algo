using System.Linq.Expressions;
using System ; 

namespace projet_algo
{
    internal class Program
    {
        static void Main(string [] args)
        {
            Console.Clear();
            Jeu session = new Jeu(8,8);
            session.BoucleJeu(session);
        }
    }
}