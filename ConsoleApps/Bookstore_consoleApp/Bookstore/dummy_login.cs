/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    internal class dummy_login
    {
        private static int userId = 0;

        private static string adminEmail = "admin@123.com";
        private static string adminPassword = "Theadmin";
        public void log()
        {
            Console.WriteLine("welcome to my signup page");
            while (true)
            {
                Console.WriteLine("\n1. Register\n2. Login\n3 Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    RegisterUser();
                }
                else if (choice == "2")
                {
                    Login();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Exit");
                    return;
                }
                else
                { Console.WriteLine("Invalid"); }

            }
        }
        public void RegisterUser()
        {
            *//* Console.WriteLine("Enter username:");
             string username = Console.ReadLine();
             Console.WriteLine("Enter Password");
             string password = Console.ReadLine();*//*
            Console.Write("Enter your credentials (username:password): ");
            *//*    string credentials = username + ":" + password;*//*
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
        public void AddNewBook()
        {
            *//*File.Create(@"E:\login\book.txt");*//*
            Console.WriteLine("Adding a new book:");
            Console.Write("Enter Book ID: ");
            string bookId = Console.ReadLine();

            Console.Write("Enter Book Name: ");
            string bookName = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            string bookPath = "E:\\login\\book.txt";
            string bookInfo = $"{bookId}:{bookName}:{description}";
            File.AppendAllText(bookPath, bookInfo);
            File.AppendAllText(bookPath, "\n");
            Console.WriteLine("New book added successfully!");
        }
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
                    DisplayBooks();
                    BuyBook();
                    return;

                }
            }
            if (newUsername == adminEmail && newPassword == adminPassword)
            {
                Console.WriteLine("Admin login success!");
                DisplayBooks();
                AddNewBook();

                return;
            }
            Console.WriteLine("failed");
        }
        public void DisplayBooks()
        {
            string booksPath = "E:\\login\\book.txt";
            var bookLines = File.ReadAllLines(booksPath);

            Console.WriteLine("List of Books:");
            foreach (var book in bookLines)
            {
                Console.WriteLine(book);
            }
        }
        public void BuyBook()
        {

            Console.Write("Enter the ID of the book you want to buy: ");
            string bookIdToBuy = Console.ReadLine();

            string booksPath = "E:\\login\\book.txt";
            var bookLines = File.ReadAllLines(booksPath);

            foreach (var book in bookLines)
            {
                string[] parts = book.Split(':');
                if (parts.Length == 3 && parts[0] == bookIdToBuy)
                {
                    Console.WriteLine($"You have successfully purchased the book: {parts[1]}");

                    string orderPath = "E:\\login\\orders.txt";
                    string orderInfo = $"{userId}:{bookIdToBuy}";
                    File.AppendAllText(orderPath, orderInfo + "\n");
                    return;
                }
            }

            Console.WriteLine("Book not found or invalid book ID. Purchase failed.");
        }
    }
}

    





*/