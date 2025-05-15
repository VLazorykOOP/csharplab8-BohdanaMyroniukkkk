
using System;
using System.IO;

public class Task4
{
    private string filePath = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\numbers.bin";

    public void Run() // Видаляємо async Task, робимо метод синхронним
    {
        try
        {
            // Крок 1: Запис чисел у двійковий файл
            Console.Write("Введiть кiлькiсть чисел (n): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                throw new ArgumentException("Кiлькiсть чисел повинна бути додатним цiлим числом.");
            }

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                Console.WriteLine("Введiть дiйснi числа:");
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Число {i + 1}: ");
                    if (double.TryParse(Console.ReadLine(), out double number))
                    {
                        writer.Write(number);
                    }
                    else
                    {
                        throw new FormatException("Введено некоректне число.");
                    }
                }
            }
            Console.WriteLine($"Послiдовнiсть записано у {filePath}");

            // Крок 2: Пошук максимального значення на непарних позиціях
            double maxOddPosition = double.MinValue;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                stream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < n; i++)
                {
                    double number = reader.ReadDouble();
                    if (i % 2 == 1) // Непарні позиції (1, 3, 5, ...)
                    {
                        maxOddPosition = Math.Max(maxOddPosition, number);
                    }
                }
            }

            Console.WriteLine($"Максимальне значення на непарних позицiях: {maxOddPosition}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.ReadKey();
    }
}