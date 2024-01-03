namespace Bookstore.Services.IServices
{
    public interface IUser
    {
       
        Task<List<User>> GetRegisterAsync();
        Task<User> GetRegisterByIdAsync(int id);
        Task<string> AddUser(AddUser newuser);
        Task<string> UpdateUser(int id, AddUser updatedUser);
        Task<string> DeleteUser(int id);


    }
}
