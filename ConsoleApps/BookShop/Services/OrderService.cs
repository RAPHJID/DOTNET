using BookShop.Services.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class OrderService : IOrder
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:3000/Order";
        public OrderService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> AddOrder(AddOrder neworder)
        {
            var content =JsonConvert.SerializeObject(neworder);
            var body=new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, body);
            if (response.IsSuccessStatusCode)
            {
                return "Order was added successfully!";
            }
            return "";
        }

        public async Task<string> DeleteOrder(int id)
        {
            var response = await _httpClient.DeleteAsync(URL+"/"+id);
            if(response.IsSuccessStatusCode)
            {
                return "Order was Deleted Successfully!";
            }
            return "";
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(URL+"/"+id);
            var content = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(content);
            if (response.IsSuccessStatusCode && order != null)
            {
                return order;
            }
            return new Order();
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<List<Order>>(content);
            if (response.IsSuccessStatusCode &&  orders != null)
            {
                return orders;
            }
            return new List<Order>();
        }

        public async Task<string> UpdateOrder(int id, AddOrder updatedOrder)
        {
            var content = JsonConvert.SerializeObject(updatedOrder);
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(URL+"/"+id, body);
            if (response.IsSuccessStatusCode)
            {
                return "Orser updated successfully!";
            }
            return "";
        }
    }
}
