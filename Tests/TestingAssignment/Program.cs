// See https://aka.ms/new-console-template for more information
using TestingAssignment;

Console.WriteLine("Hello, World!");
Trainee tr = new Trainee();
try
{
    tr.addStudent("Jid");
    tr.addStudent("Jed");
    tr.addStudent("Jid");
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
try
{
    tr.removeStudent("J");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);

}
