using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services.IService
{
    public interface IBook
    {
        Task<List<Book>> GetBooksAsync();

        Task<Book> GetBookByIdAsync(int id);

        Task<string> AddBook(AddBook newbook);

        Task<string> UpdateBook(int id, AddBook updatedBook);

        Task<string> DeleteBook(int id);
    }
}
