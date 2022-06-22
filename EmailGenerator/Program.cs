using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (generatorEmail())
            {
                Console.WriteLine("Email list created successfully!");
            }
            Console.ReadKey();
        }
        static List<string> createListNames()
        {
            var listNames = new List<string>();
            try
            {
                StreamReader sr = new StreamReader("names.txt");
                string name;
                while ((name = sr.ReadLine()) != null)
                {
                    listNames.Add(name);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find the file NAMES.txt");
            }

            return listNames;
        }

        static bool generatorEmail()
        {
            Random random = new Random();
            Regex reg = new Regex("[*'\",_&#^@]");
            var listEmail = new List<string>();
            StreamWriter sw = new StreamWriter("emails.txt");

            var listName = createListNames();

            if (listName.Count() == 0)
            {
                return false;
            }

            string dnsEmail = "@gmail.com";
            Console.Write("How many emails do you want to generate ? ");
            int amount = int.Parse(Console.ReadLine());

            while (listEmail.Count < amount)
            {
                int number_to_differentiate = random.Next(9999);
                int positonListRandom = random.Next(listName.Count);
                string email = reg.Replace(listName[positonListRandom], "") + number_to_differentiate.ToString() + dnsEmail;

                if (!listEmail.Contains(email))
                {
                    listEmail.Add(email);
                }
            }
            listEmail.ForEach(email => sw.WriteLine(email));
            sw.Close();
            return true;
        }
    }
}
