using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories
{
    public interface IExpenseRepository
    {
        List<Expense> GetAll();
        void Delete(int id);
        void SaveToFile();
        void AddExpense(Expense expense);
        int GetNextId();
    }
}
