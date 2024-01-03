using BookShop.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class UserController
    {
        private const string AdminUsername = "Theadmin";
        private const string AdminPassword = "AdminPass";

        UserService userService = new UserService();
        BookController bookController = new BookController();
       

        public async Task RegisterUserUI()
        {
            Console.WriteLine("Register a new user");

            Console.WriteLine(" UserName: ");
            var userName = Console.ReadLine();

            Console.WriteLine(" Password: ");
            var password = Console.ReadLine();

            var newUser = new AddUser() { UserName = userName, Password = password };
            await AddUserRequest(newUser);

            // were user should be redirected
        }
        public async Task LoginUserUI()
        {
            Console.WriteLine("Login");

            Console.WriteLine(" UserName: ");
            var userName = Console.ReadLine();

            Console.WriteLine(" Password: ");
            var password = Console.ReadLine();



            if (IsAdminLogin(userName, password))
            {
                await bookMenu();

                //await init();
                //redirect
            }
            else
            {


                var isvalidUser = await userService.ValidateUser(userName, password);
                if (isvalidUser)
                {
                    Console.WriteLine("User login success!");
                }

                else
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                    //back to login

                }
            }
        }
        private bool IsAdminLogin(string userName, string password)
        {
            return userName == AdminUsername && password == AdminPassword;
        }

        public async Task start()
        {
            Console.WriteLine("1. Register:");
            Console.WriteLine("2. Login: ");
            Console.WriteLine("3. Exit: ");
            var select = Console.ReadLine();
            Console.WriteLine(select);


            var accept = int.TryParse(select, out int direct);
            await redirectUser(direct);

        }
        public async Task init()
        {
            Console.WriteLine("1. Add a new User");
            Console.WriteLine("2. Get all Users");
            Console.WriteLine("3. Update a User");
            Console.WriteLine("4. Delete a User");
            Console.WriteLine("Enter your input:");
            var input = Console.ReadLine();
            Console.WriteLine(input);


            var output = int.TryParse(input, out int option);

           // await redirectUser(option);
        }

        public async Task redirectUser(int option)
        {
            switch (option)
            {
                case 1:
                    await RegisterUserUI();
                    break;

                case 2:
                    await LoginUserUI();
                    break;
                case 3:
                    Console.WriteLine("Exited");
                    break;
                

            }

        }

        public async Task AddUserUI()
        {
            Console.WriteLine("Add a new user");

            Console.WriteLine(" UserName: ");
            var UserName = Console.ReadLine();

            Console.WriteLine(" Password: ");
            var Password = Console.ReadLine();


            var newUser = new AddUser() { UserName = UserName, Password = Password};
            await AddUserRequest(newUser);
            
        }

        public async Task AddUserRequest(AddUser newUser)
        {

            var response = await userService.AddUser(newUser);
            Console.WriteLine(response);
            await start();
        }

        public async Task showUsers()
        {
            var users = await userService.GetUsersAsync();
            Console.WriteLine($"Id \t UserName \t Password");
            foreach (var user in users)
            {
                Console.WriteLine($" {user.Id} \t {user.UserName} \t {user.Password} ");
            }
        }

        public async Task updateUserUI()
        {
            await showUsers();
            Console.WriteLine("Select User Update by Id :");
            var use = Console.ReadLine();
            var output = int.TryParse(use, out int UserId);
            Console.WriteLine("  User Name: ");
            var UserName = Console.ReadLine();

            Console.WriteLine(" UserName Password: ");
            var Password = Console.ReadLine();


            var updatedUser = new AddUser() { UserName = UserName, Password = Password};

            await updateUserRequest(UserId, updatedUser);
        }


        public async Task updateUserRequest(int UserId, AddUser updatedUser)
        {
            var response = await userService.UpdateUser(UserId, updatedUser);
            Console.WriteLine(response);
            await init();
        }

        public async Task deleteUser()
        {
            await showUsers();
            Console.WriteLine("Select User to Delete by Id :");
            var use = Console.ReadLine();
            var output = int.TryParse(use, out int UserId);
            var response = await userService.DeleteUser(UserId);
            Console.WriteLine(response);
        }


    }
}
