using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Lab8T2
{
    private string inputFilePath;
    private string outputFilePath;
    private List<string> coordinates;
    private const string CoordinatePattern = @"\d{1,3}\.\d{1,4}[°](N|S),\s*\d{1,3}\.\d{1,4}[°](E|W)";

    public Lab8T2(string inputPath = "input.txt", string outputPath = "output.txt")
    {
        inputFilePath = inputPath;
        outputFilePath = outputPath;
        coordinates = new List<string>();
    }

    public async Task Run()
    {
        try
        {
            // Виконуємо завдання асинхронно
            await Task.WhenAll(
                ReadAndExtractCoordinatesAsync(),
                Task.Run(() => Console.WriteLine("Початок обробки файлу..."))
            );

            // Записуємо координати у вихідний файл
            await WriteCoordinatesToFileAsync();

            // Виводимо кількість знайдених координат
            Console.WriteLine($"\nЗнайдено координат: {coordinates.Count}");

            // Інтерактивна частина: заміна або видалення координат
            await ProcessUserCommandsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    private async Task ReadAndExtractCoordinatesAsync()
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException("Вхідний файл не знайдено.");
        }

        string text = await File.ReadAllTextAsync(inputFilePath);
        var matches = Regex.Matches(text, CoordinatePattern);

        coordinates.Clear();
        foreach (Match match in matches)
        {
            coordinates.Add(match.Value);
        }
    }

    private async Task WriteCoordinatesToFileAsync()
    {
        await File.WriteAllLinesAsync(outputFilePath, coordinates);
        Console.WriteLine($"Координати записано у файл: {outputFilePath}");
    }

    private async Task ProcessUserCommandsAsync()
    {
        while (true)
        {
            Console.WriteLine("\nВиберіть дію:");
            Console.WriteLine("1. Замінити координати");
            Console.WriteLine("2. Видалити координати");
            Console.WriteLine("3. Вийти");
            Console.Write("Ваш вибір (1-3): ");

            string choice = Console.ReadLine();

            if (choice == "3") break;

            if (choice == "1" || choice == "2")
            {
                Console.Write("Введіть координати для обробки (наприклад, 40.7128°N, 74.0060°W): ");
                string target = Console.ReadLine();

                if (!Regex.IsMatch(target, CoordinatePattern))
                {
                    Console.WriteLine("Невірний формат координат!");
                    continue;
                }

                if (choice == "1")
                {
                    Console.Write("Введіть нові координати: ");
                    string replacement = Console.ReadLine();
                    if (!Regex.IsMatch(replacement, CoordinatePattern))
                    {
                        Console.WriteLine("Невірний формат нових координат!");
                        continue;
                    }

                    await ReplaceCoordinatesAsync(target, replacement);
                }
                else
                {
                    await RemoveCoordinatesAsync(target);
                }
            }
            else
            {
                Console.WriteLine("Невірний вибір!");
            }
        }
    }

    private async Task ReplaceCoordinatesAsync(string target, string replacement)
    {
        int index = coordinates.IndexOf(target);
        if (index >= 0)
        {
            coordinates[index] = replacement;
            await WriteCoordinatesToFileAsync();
            Console.WriteLine($"Координати {target} замінено на {replacement}.");
        }
        else
        {
            Console.WriteLine($"Координати {target} не знайдено.");
        }
    }

    private async Task RemoveCoordinatesAsync(string target)
    {
        if (coordinates.Remove(target))
        {
            await WriteCoordinatesToFileAsync();
            Console.WriteLine($"Координати {target} видалено.");
        }
        else
        {
            Console.WriteLine($"Координати {target} не знайдено.");
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        Lab8T2 lab8task2 = new Lab8T2();
        await lab8task2.Run();
    }
}