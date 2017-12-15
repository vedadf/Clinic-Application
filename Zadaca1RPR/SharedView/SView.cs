using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharedView
{
    public static class SView
    {

        public static bool ValidCitizenID(string input, out string error)
        {
            if (input.Length != 13)
            {
                error = "Velicina mora biti 13";
                return false;
            }
            bool parse = true;
            //da li su svi brojevi
            foreach (char c in input)
            {
                int n;
                parse = Int32.TryParse(c.ToString(), out n);
                if (!parse)
                {
                    error = "JMBG mora sadrzavati samo brojeve";
                    return false;
                }
            }

            //da li je datum ispravan
            string day = "" + input[0] + input[1];
            string month = "" + input[2] + input[3];
            string year = "" + input[4] + input[5] + input[6] + input[7];
            string Bdate = year + "-" + month + "-" + day;
            DateTime bDate = new DateTime();
            if (!DateTime.TryParse(Bdate, out bDate))
            {
                error = "Datum nije ispravan";
                return false;
            }
            error = "";
            return true;
        }

        public static bool ValidDate(string input, out string error)
        {

            if(input.Length != 10)
            {
                error = "Velicina polja mora biti 10";
                return false;
            }

            if(input[2] != '-' || input[5] != '-')
            {
                error = "Format nije validan";
                return false;
            }
                        
            string day = "" + input[0] + input[1];
            string month = "" + input[3] + input[4];
            string year = "" + input[6] + input[7] + input[8] + input[9];
            string Bdate = year + "-" + month + "-" + day;
            DateTime bDate = new DateTime();
            if (!DateTime.TryParse(Bdate, out bDate))
            {
                error = "Datum nije ispravan";
                return false;
            }

            error = "";
            return true;
        }

        public static bool ValidTime(string time, out string error)
        {
            if(time.Length != 5)
            {
                error = "Velicina mora biti 5";
                return false;
            }

            if(time[2] != ':')
            {
                error = "Format nije validan";
                return false;
            }

            string hour = "" + time[0] + time[1];
            string minute = "" + time[3] + time[4];
            int hh, mm;
            if(!Int32.TryParse(hour, out hh))
            {
                error = "Uneseni sat nije broj";
                return false;
            }

            if(!Int32.TryParse(minute, out mm))
            {
                error = "Unesene minute nisu broj";
                return false;
            }

            if(hh > 23 || hh < 0 || mm < 0 || mm > 59)
            {
                error = "Parametri vremena van opsega";
                return false;
            }
            error = "";
            return true;
             
        }

        public static void NoCommand()
        {
            Console.WriteLine("Komanda ne postoji.");
        }

        public static void WaitInput()
        {
            Console.WriteLine("Unesite bilo sta za otvaranje menija.");
            Console.ReadLine();
        }

        public static void PrintList<T>(List<T> list)
        {
            foreach (var item in list)
                Console.WriteLine(item);
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

        public static string GetHash(MD5 md, string pw)
        {
            byte[] data = md.ComputeHash(Encoding.UTF8.GetBytes(pw));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
        
        public static bool VerifyHash(MD5 md, string input, string hash)
        {
            string HashInput = GetHash(md, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(HashInput, hash))
                return true;

            return false;
        }

        public static bool HasOnlyLetters(string s, out string error)
        {
            if(s == null || s == "")
            {
                error = "Polje ne smije biti prazno";
                return false;
            }

            if (s.All(Char.IsLetter))
            {                 
                error = "";
                return true;
            }
            error = "Polje mora sadrzavati samo slova";
            
            return false;
        }

        public static bool HasOnlyLettersAndDigits(string s, out string error)
        {
            if (s == null || s == "")
            {
                error = "Polje ne smije biti prazno";
                return false;
            }

            if (s.All(Char.IsLetterOrDigit))
            {
                error = "";
                return true;
            }
            error = "Polje mora sadrzavati slova ili brojeve";
            return false;
        }

        public static bool HasOnlyLettersAndDigitsAllowSpace(string s, out string error)
        {
            foreach (char c in s)
            {
                if (c != ' ' && !Char.IsLetterOrDigit(c))
                {
                    error = "Polje mora sadrzavati slova ili brojeve";
                    return false;
                }
            }
            error = "";
            return true;           
        }
        
    }
}
