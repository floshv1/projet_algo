using System.Linq.Expressions;
using System ; 

namespace projet_algo
{
    internal class Program
    {
        static void Main(string [] args)
        {
            Console.Clear();
            /*Jeu session = new Jeu(8,8);
            Jeu.AuTourDe(session,"joueur1");
            session.Jouer(session.joueur1);
            session.SaveGameToCSV($"Save/save{session.nom}.csv");*/

            Jeu session = new Jeu("Save/saveTest.csv");
            Jeu.AuTourDe(session,"joueur2");
            session.Jouer(session.joueur2);
        }
    }
}