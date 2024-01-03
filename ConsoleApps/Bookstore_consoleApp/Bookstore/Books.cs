using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    internal class Books
    {
        private static int userId = 0;
        public void AddNewBook()
        {
            /*File.Create(@"E:\login\book.txt");*/
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
