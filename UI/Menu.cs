using ExpenseTracker.Services;

namespace ExpenseTracker.UI
{
    public class Menu
    {
        private readonly ExpenseService _service;

        public Menu(ExpenseService service)
        {
            _service = service;
        }

        public void Run()
        {
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine() ?? "";

                if (choice == "1") AddExpense();
                else if (choice == "2") ShowAllExpenses();
                else if (choice == "3") ShowExpensesByCategory();
                else if (choice == "4") DeleteExpense();
                else if (choice == "5") { Console.WriteLine("Пока!"); break; }
                else { Console.WriteLine("Неверный выбор."); }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Учёт расходов ===");
            Console.WriteLine("\n1. Добавить расход");
            Console.WriteLine("2. Показать все");
            Console.WriteLine("3. Показать по категориям");
            Console.WriteLine("4. Удалить запись");
            Console.WriteLine("5. Выход");
            Console.Write("> ");
        }

        private void AddExpense()
        {
            Console.Write("Сумма: ");
            string ?input = Console.ReadLine();

            if (!decimal.TryParse(input, out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Ошибка: Неверный формат суммы!");
                return;
            } 

            Console.Write("Категория: ");
            string category = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine("Ошибка: Категория не может быть пустой!");
                return;
            }

            _service.AddExpense(amount, category);

            Console.WriteLine("Запись успешно добавлена!");
        }

        private void ShowAllExpenses()
        {
            _service.GetAll();
        }

        private void ShowExpensesByCategory()
        {
            Console.Write("Введите категорию: ");
            string cat = Console.ReadLine() ?? "";
            _service.GetByCategory(cat);
        }

        private void DeleteExpense()
        {
            Console.Write("Введите ID записи для удаления: ");
            string idInput = Console.ReadLine() ?? "";
            if (int.TryParse(idInput, out int id))
            {
                _service.Delete(id);
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID!");
            }
        }
    }
}
