using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedView
{
    public static class SView
    {
        
        public static void NoCommand()
        {
            Console.WriteLine("Komanda ne postoji.");
        }

        public static void WaitInput()
        {
            Console.WriteLine("Unesite bilo sta za otvaranje menija.");
            Console.ReadLine();
        }

    }
}
