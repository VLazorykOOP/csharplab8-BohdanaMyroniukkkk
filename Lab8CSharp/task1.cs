using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Task1
{
    private List<string> coordinates = new List<string>();
    private const string CoordinatePattern = @"\d{1,3}\.\d{1,4}[°](N|S),\s*\d{1,3}\.\d{1,4}[°](E|W)";
    private string inputFilePath = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\input.txt";
    private string outputFilePath = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\output.txt";

    public async Task Run()
    {
        try
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Файл {inputFilePath} не знайдено.");
                Console.WriteLine("Бажаєте створити порожнiй файл? (y/n)");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    await File.WriteAllTextAsync(inputFilePath, "");
                    Console.WriteLine($"Створено порожнiй файл {inputFilePath}.");
                }
                else
                {
                    throw new FileNotFoundException("Вхiдний файл не знайдено.");
                }
            }

            string text = await File.ReadAllTextAsync(inputFilePath);
            var matches = Regex.Matches(text, CoordinatePattern);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    coordinates.Add(match.Value.Trim());
                }
            }

            await File.WriteAllLinesAsync(outputFilePath, coordinates);
            Console.WriteLine($"Знайдено координат: {coordinates.Count}");
            if (coordinates.Count > 0)
            {
                Console.WriteLine("Список знайдених координат:");
                foreach (var coord in coordinates)
                {
                    Console.WriteLine($" - {coord}");
                }
            }

            while (true)
            {
                Console.WriteLine("\nДiї з координатами:");
                Console.WriteLine("1. Замiнити координати");
                Console.WriteLine("2. Видалити координати");
                Console.WriteLine("3. Повернутися до меню");
                Console.Write("Ваш вибiр (1-3): ");
                string? action = Console.ReadLine();
                if (action == "3") break;

                if (action == "1" || action == "2")
                {
                    Console.Write("Введiть координати для обробки: ");
                    string? target = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(target) || !Regex.IsMatch(target, CoordinatePattern))
                    {
                        Console.WriteLine("Невiрний формат координат!");
                        continue;
                    }

                    if (action == "1")
                    {
                        Console.Write("Введiть новi координати: ");
                        string? replacement = Console.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(replacement) || !Regex.IsMatch(replacement, CoordinatePattern))
                        {
                            Console.WriteLine("Невiрний формат нових координат!");
                            continue;
                        }
                        int index = coordinates.IndexOf(target);
                        if (index >= 0)
                        {
                            coordinates[index] = replacement;
                            await File.WriteAllLinesAsync(outputFilePath, coordinates);
                            Console.WriteLine($"Координати {target} замiнено на {replacement}.");
                        }
                        else
                        {
                            Console.WriteLine($"Координати {target} не знайдено.");
                        }
                    }
                    else
                    {
                        if (coordinates.Remove(target))
                        {
                            await File.WriteAllLinesAsync(outputFilePath, coordinates);
                            Console.WriteLine($"Координати {target} видалено.");
                        }
                        else
                        {
                            Console.WriteLine($"Координати {target} не знайдено.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.ReadKey();
    }
}