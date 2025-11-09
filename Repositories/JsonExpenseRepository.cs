using ExpenseTracker.Models;
using System.Text.Json;

namespace ExpenseTracker.Repositories
{
    class JsonExpenseRepository : IExpenseRepository
    {
        private readonly string filePath = "expenses.json";
        private List<Expense> _expenses = new List<Expense>();

        public JsonExpenseRepository()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath).Trim();

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        _expenses = new List<Expense>();
                    }
                    else
                    {
                        var deserialized = JsonSerializer.Deserialize<List<Expense>>(json);
                        _expenses = deserialized ?? new List<Expense>();
                    }
                }
                catch (Exception ex) when (ex is JsonException || ex is IOException)
                {
                    Console.WriteLine("Внимание: файл expenses.json повреждён или пуст. Создаём новый.");
                    _expenses = new List<Expense>();
                }
            }
            else
            {
                _expenses = new List<Expense>();
            }
        }

        public List<Expense> GetAll()
        {
            return _expenses;
        }

        public void SaveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(_expenses));
        }

        // Исправлено: правильная сигнатура метода
        public void AddExpense(Expense expense)
        {
            expense.Id = GetNextId();
            _expenses.Add(expense);
            SaveToFile();
        }

        public void Delete(int id)
        {
            var expense = _expenses.FirstOrDefault(e => e.Id == id);
            if (expense != null)
            {
                _expenses.Remove(expense);
                SaveToFile();
            }
        }

        // Исправлено: реализация метода GetNextId
        public int GetNextId()
        {
            return _expenses.Count > 0 ? _expenses.Max(e => e.Id) + 1 : 1;
        }
    }
}
