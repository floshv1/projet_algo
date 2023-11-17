namespace projet_algo // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire dico = new Dictionnaire("Français");
            Console.WriteLine(dico.toString());       
        }
    }
}