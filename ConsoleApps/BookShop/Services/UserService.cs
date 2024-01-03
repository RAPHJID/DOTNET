using BookShop.Services.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class UserService : IUser
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:3000/User";
        public UserService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> AddUser(AddUser newuser)
        {
            var content = JsonConvert.SerializeObject(newuser);
            var body=new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, body);
            if (response.IsSuccessStatusCode)
            {
                return "User added Successfully!";
            }
            return "";
        }

        public async Task<string> DeleteUser(int id)
        {
            var response= await _httpClient.DeleteAsync(URL+"/"+id);
            if (response.IsSuccessStatusCode)
            {
                return "User was Deleted Successfully!!";
            }
            return "";
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(URL+"/"+id);
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            if (response.IsSuccessStatusCode && user != null)
            {
                return user;
            }
            return new User();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(content);
            if (response.IsSuccessStatusCode && users != null)
            {
                return users;
            }
            return new List<User>();
        }

        public async Task<string> UpdateUser(int id, AddUser updatedUser)
        {
            var content = JsonConvert.SerializeObject(updatedUser);
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(URL+"/"+id, body);
            if (response.IsSuccessStatusCode)
            {
                return "User Updated Successfully!";
            }
            return "";
        }

        public Task<bool> ValidateUser(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }

}
