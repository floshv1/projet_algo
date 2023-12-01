using System.Linq.Expressions;
using System ; 

namespace projet_algo
{
    internal class Program
    {
        static void Main(string [] args)
        {
           Deplacement a  = new Deplacement(10, 20);
           Console.WriteLine(a.toString());
           a.Gauche();
           Console.WriteLine(a.toString());
           a.Droite();
           Console.WriteLine(a.toString());
           a.DiagDroite();
           Console.WriteLine(a.toString());
           a.DiagGauche();
           Console.WriteLine(a.toString());
           a.Haut();
           Console.WriteLine(a.toString());

        }
    }
}