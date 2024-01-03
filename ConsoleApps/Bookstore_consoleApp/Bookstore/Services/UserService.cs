using Bookstore.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class UserService : IUser
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:3000/users";

        public UserService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> AddUser(AddUser newuser)
        {
            var content = Json.Convert.SerializeObject(newuser);
            var body=new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, body);
            if (response.IsSuccessStatusCode)
            {
                return "Users is added successfully";
            }
            return "";
        }

        public async Task<string> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync(URL + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                return "User was deleted successfully!";
            }
            return "";
        }

        public async Task<List<User>> GetRegisterAsync()
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(content);
            if(response.IsSuccessStatusCode && users != null) {
                return users;
            }
            return  new List<User> { new User() };

        }

        public Task<User> GetRegisterByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(URL + "/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            if (response.IsSuccessStatusCode && user != null)
            {
                return user;
            }
            return new User();
        }

        public Task<string> UpdateUser(int id, AddUser updatedUser)
        {
            var content = JsonConvert.SerializeObject(updatedUser);
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(URL + "/" + id, body);
            if (response.IsSuccessStatusCode)
            {
                return "User was updated successfully!";
            }
            return "";
        }
    }
}
