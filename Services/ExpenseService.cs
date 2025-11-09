using System.Text.Json;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        private readonly IExpenseRepository _repo;

        public ExpenseService(IExpenseRepository repo)
        {
            _repo = repo;
        }

        // Добавить расход
        public void AddExpense(decimal amount, string category)
        {
            var expense = new Expense
            {
                Id = _repo.GetNextId(),
                Amount = amount,
                Category = category,
                Date = DateTime.Now
            };

            _repo.AddExpense(expense);
        }

        // Показать все
        public void GetAll()
        {
            var expenses = _repo.GetAll();

            if (expenses.Count == 0)
            {
                Console.WriteLine("Нет записей.");
                return;
            }

            foreach (var e in expenses)
            {
                Console.WriteLine($"[{e.Id}] {e.Amount} грн — {e.Category} — {e.Date:dd.MM.yyyy}");
            }
        }

        // Показать по категории
        public void GetByCategory(string category)
        {
            var expenses = _repo.GetAll();
            var filtered = expenses.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine($"Нет расходов в категории: {category}");
                return;
            }

            decimal total = 0;
            foreach (var e in filtered)
            {
                Console.WriteLine($"  {e.Amount} грн — {e.Date:dd.MM.yyyy}");
                total += e.Amount;
            }
            Console.WriteLine($"Итого по «{category}»: {total} грн");
        }

        // Удалить запись
        public void Delete(int id)
        {
            _repo.Delete(id);
            Console.WriteLine($"Запись с ID {id} удалена.");
        }
    }
}