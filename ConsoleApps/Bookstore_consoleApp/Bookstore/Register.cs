using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    internal class Register
    {

        private static int userId = 0;

        public void RegisterUser()
        {
            /* Console.WriteLine("Enter username:");
             string username = Console.ReadLine();
             Console.WriteLine("Enter Password");
             string password = Console.ReadLine();*/
            Console.Write("Enter your credentials (username:password): ");
            /*    string credentials = username + ":" + password;*/
            string credentials = Console.ReadLine();


            if (string.IsNullOrEmpty(credentials) || !credentials.Contains(":"))
            {
                Console.WriteLine("INVALID");
                return;
            }
            string[] parts = credentials.Split(':');

            if (parts.Length != 2)
            {
                Console.WriteLine("invalid format");
                return;
            }
            int id = ++userId;
            string username = parts[0];
            string password = parts[1];
            //increment


            string path = "E:\\login\\data.txt";
            string info = $"{id}:{username}:{password}";
            File.AppendAllText(path, info);
            File.AppendAllText(path, "\n");
            Console.WriteLine("Success");

        }
    }
}
