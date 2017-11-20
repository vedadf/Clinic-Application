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

        public static bool ValidateCitizenID(DateTime bdayy, string id)
        {
            bool parse = true;
            //da li su svi brojevi
            foreach (char c in id)
            {
                int n;
                parse = Int32.TryParse(c.ToString(), out n);
                if (!parse) return false;
            }

            //da li je datum ispravan
            string day = "" + id[0] + id[1];
            string month = "" + id[2] + id[3];
            string year = "" + id[4] + id[5] + id[6] + id[7];
            string Bdate = year + "-" + month + "-" + day;
            DateTime bDate = new DateTime();
            if (!DateTime.TryParse(Bdate, out bDate)) return false;

            //da li se datum rodjenja poklapa           
            if (bdayy != bDate) return false;

            return true;

        }

        public static bool ValidateCitizenIDReg(List<string> Cids, DateTime bdayy, string id)
        {
            if (!ValidateCitizenID(bdayy, id)) return false;

            if (Cids.Find(i => i == id) == null) return true;

            return false;

        }

    }
}
