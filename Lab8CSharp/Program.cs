using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nВиберіть завдання:");
            Console.WriteLine("1. Завдання 1: Обробка географічних координат");
            Console.WriteLine("2. Завдання 2");
            Console.WriteLine("3. Завдання 3");
            Console.WriteLine("4. Завдання 4");
            Console.WriteLine("5. Завдання 5: Робота з файлами та папками");
            Console.WriteLine("6. Вихід");
            Console.Write("Ваш вибір (1-6): ");

            string? choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice)) continue;

            switch (choice)
            {
                case "1":
                    await new Task1().Run();
                    break;
                case "2":
                    await new Task2().Run();
                    break;
                case "3":
                    await new Task3().Run();
                    break;
                case "4":
                    new Task4().Run();
                    break;
                case "5":
                    await new Task5().Run();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Неправильний вибір! Спробуйте ще раз.");
                    break;
            }
        }
    }
}