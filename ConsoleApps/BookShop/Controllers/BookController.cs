using BookShop.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class BookController
    {
        BookService bookService = new BookService();

        public async Task bookMenu()
        {
            Console.WriteLine("1. Add a new Book:");
            Console.WriteLine("2. Get all Books:");
            Console.WriteLine("3. Update a Book:");
            Console.WriteLine("4. Delete a Book: ");
            Console.WriteLine("Enter your choice: ");
            var choice = Console.ReadLine();
            Console.WriteLine(choice);

            var output = int.TryParse(choice, out int option);

            await redirectMenu(option);
        }
        public async Task redirectMenu(int option)
        {
            switch (option)
            {
                case 1:
                    await AddBookUI();
                    break;
                case 2:
                    await showBooks();
                    break;

                case 4:
                    await deleteBook();
                    break;
            }
        }
        public async Task AddBookUI()
        {
            Console.WriteLine("Add a new Book");
            Console.WriteLine("Book Title: ");
            var Title = Console.ReadLine();
            Console.WriteLine("Book Price: ");
            var Pricestr = Console.ReadLine();
            var res = int.TryParse(Pricestr, out int Price);

            var newBook = new AddBook() { Title = Title, Price = Price };

            await AddBookRequest(newBook);

        }
        public async Task AddBookRequest(AddBook newBook)
        {
            var response = await bookService.AddBook(newBook);
            Console.WriteLine(response);
            await bookMenu();
        }
        public async Task showBooks()
        {
            var books = await bookService.GetBooksAsync();
            Console.WriteLine($"Id \t Title \t Price");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.BookId} \t {book.Title} \t {book.Price}");
            }
        }

        public async Task updatedBookRequest(int bookId, AddBook updatedBook)
        {
            var response = await bookService.UpdateBook(bookId, updatedBook);
            Console.WriteLine(response);
            await bookMenu();

        }
        public async Task deleteBook()
        {
            await showBooks();
            Console.WriteLine("Select the book id to delete:");
            var bk = Console.ReadLine();
            var output = int.TryParse(bk, out int BookId);
            var response = await bookService.DeleteBook(BookId);
            Console.WriteLine(response);
        }
    }
}
