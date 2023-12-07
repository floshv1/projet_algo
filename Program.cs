using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading;

namespace projet_algo
{
    class Program
    {
        public static Jump jump = Jump.Continue;
        static void Main()
        {
            Interface.Affichage();

            Main_Menu :
            Interface.MainMenu();
            if (jump != Jump.Continue) goto Selection;

            Selection :

            switch(jump)
            {
                case Jump.Continue:
                    break;
                case Jump.Main_Menu:
                    jump = Jump.Continue;
                    goto Main_Menu;
            }
        }
        public enum Jump
        {
            Continue,
            Main_Menu
        }
    }
}
