// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Hello, World!");*/
using Bookstore;


Console.WriteLine("welcome to my signup page");
while (true)
{
    Console.WriteLine("\n1. Register\n2. Login\n3 Exit");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();
    if (choice == "1")
    {
        Register reg =new Register();
        reg.RegisterUser();
    }
    else if (choice == "2")
    {
        Log_in log = new Log_in();
        log.Login();
    }
    else if (choice == "3")
    {
        Console.WriteLine("Exit");
        return;
    }
    else
    { Console.WriteLine("Invalid"); }

}


