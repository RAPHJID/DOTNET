using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services.IService
{
    public interface IOrder
    {
        Task<List<Order>> GetOrdersAsync();

        Task<Order> GetOrderByIdAsync(int id);

        Task<string> AddOrder(AddOrder neworder);

        Task<string> UpdateOrder(int id, AddOrder updatedOrder);

        Task<string> DeleteOrder(int id);
    }
}
