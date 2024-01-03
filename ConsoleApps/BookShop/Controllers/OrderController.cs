using BookShop.Services;
using BookShop.Services.IService;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class OrderController
    {
        OrderService orderService = new OrderService();

        public async Task orderMenu()
        {
            Console.WriteLine("1. Add a new Order");
            Console.WriteLine("2. Get all Orders");
            Console.WriteLine("3. Update an order");
            Console.WriteLine("4. Delete an Order");
            Console.WriteLine("Enter your input:");
            var input = Console.ReadLine();
            Console.WriteLine(input);

            var output = int.TryParse(input, out int option);
      
            await redirectUser(option);
        }

        public async Task redirectUser(int option)
        {
            switch (option)
            {
                case 1:
                    await AddOrderUI();
                    break;

                case 2:
                    await showOrders();
                    break;
                case 3:
                    await updateOrderUI();
                    break;
                case 4:
                    await deleteOrder();
                    break;

            }

        }

        public async Task AddOrderUI()
        {
            Console.WriteLine("Add a new  Order");

            Console.WriteLine("UserId:");
            var userStr = Console.ReadLine();
            var res = int.TryParse(userStr, out int UserId);

            Console.WriteLine(" Book Id: ");
            var bookStr = Console.ReadLine();
            var resp = int.TryParse(bookStr, out int BookId);

            var newOrder = new AddOrder() { UserId = UserId, BookId = BookId };
            await AddOrderRequest(newOrder);


        }
        public async Task AddOrderRequest(AddOrder newOrder)
        {
            var response = await orderService.AddOrder(newOrder);
            Console.WriteLine(response);
            await orderMenu();
        }

    

        public async Task showOrders()
        {
            var orders = await orderService.GetOrdersAsync();
            Console.WriteLine($"UserId \t BookId");
            foreach (var order in orders)
            {
                Console.WriteLine($" {order.Id} \t {order.UserId} \t {order.BookId} ");
            }
        }

        public async Task updateOrderUI()
        {
            await showOrders();
            Console.WriteLine("Select Order Update by Id :");
            var ord = Console.ReadLine();
            var output = int.TryParse(ord, out int OrderId);
            Console.WriteLine("  User Id: ");
            var userStr = Console.ReadLine();
            var res = int.TryParse(userStr, out int UserId);
           //var updatedOrder = new AddOrder() { BookId = BookId, UserId = UserId };

        }


        public async Task updateOrderRequest(int orderId, AddOrder updatedOrder)
        {
            var response = await orderService.UpdateOrder(orderId, updatedOrder);
            Console.WriteLine(response);
            await orderMenu();
        }

        public async Task deleteOrder()
        {
            await showOrders();
            Console.WriteLine("Select Order to Delete by Id :");
            var ord = Console.ReadLine();
            var output = int.TryParse(ord, out int OrderId);
            var response = await orderService.DeleteOrder(OrderId);
            Console.WriteLine(response);
        }
    }
}
