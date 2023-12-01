namespace projet_algo // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire dico = new Dictionnaire("Français");
            Console.WriteLine(dico.toString());

            /*foreach (string ligne in dico.ListeMots[24])
            {
                Console.Write(ligne + " ");
            }

            Console.WriteLine("\n");

            dico.Tri_Fusion_Dico();
            foreach (string ligne in dico.ListeMots[24])
            {
                Console.Write(ligne + " ");
            }

            Console.WriteLine("\n");

            Console.WriteLine(dico.RecheDichoDico("Yod"));*/
        }
    }
}