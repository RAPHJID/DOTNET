using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    internal class Log_in
    {
        private static string adminEmail = "admin@123.com";
        private static string adminPassword = "Theadmin";

        Books books= new Books();
        public void Login()

        {
            Console.WriteLine("Enter username:");
            string newUsername = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string newPassword = Console.ReadLine();

            string path = "E:\\login\\data.txt";
            var lines = File.ReadAllLines(path);

            foreach (var item in lines)
            {
                string[] parts = item.Split(':');

                if (parts.Length == 3 && parts[1] == newUsername && parts[2] == newPassword)
                {
                    int Id = int.Parse(parts[0]);
                    Console.WriteLine(" User login success!");

                    books.DisplayBooks();
                    books.BuyBook();
                    return;

                }
            }
            if (newUsername == adminEmail && newPassword == adminPassword)
            {
                Console.WriteLine("Admin login success!");
                books.DisplayBooks();
                books.AddNewBook();

                return;
            }
            Console.WriteLine("failed");
        }
    }
}
