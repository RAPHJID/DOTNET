using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BookShop.Services.IService
{
    public interface IUser
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<string> AddUser(AddUser newuser);

        Task<string> UpdateUser(int id, AddUser updatedUser);

        Task<string> DeleteUser(int id);
    }
}
