using BookShop.Services.IService;
using Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace BookShop.Services
{
    public class BookService : IBook
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:3000/Book";
        public BookService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> AddBook(AddBook newbook)
        {

            var content = JsonConvert.SerializeObject(newbook);
            var body=new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, body);
            if (response.IsSuccessStatusCode)
            {
                return "Book was Added successfully!";
            }
            return "";
        }

        public async Task<string> DeleteBook(int id)
        {
            var reponse = await _httpClient.DeleteAsync(URL+"/"+id);
            if (reponse.IsSuccessStatusCode)
            {
                return "Book Deleted Successfully!!";
            }
            return "";
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(URL+"/"+id);
            var content = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);
            if (response.IsSuccessStatusCode && book != null )
            {
                return book;
            }
            return new Book();
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);
            if (response.IsSuccessStatusCode && books != null )
            {
                return books;
            }
            return new List<Book>();
        }

        public async Task<string> UpdateBook(int id, AddBook updatedBook)
        {
            var content = JsonConvert.SerializeObject(updatedBook);
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(URL+"/"+id, body);
            if (response.IsSuccessStatusCode)
            {
                return "Book Updated Successfully!!";
            }
            return "";
        }
    }
}
