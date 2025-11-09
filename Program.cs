using ExpenseTracker.UI;
using ExpenseTracker.Repositories;
using ExpenseTracker.Services;


class Program
{
    static void Main(string[] args)
    {
        var repository = new JsonExpenseRepository();
        var service = new ExpenseService(repository);
        var menu = new Menu(service);
        menu.Run();
    }
}
