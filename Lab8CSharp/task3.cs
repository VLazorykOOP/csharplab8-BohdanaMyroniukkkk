using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class Task3
{
    private string inputFilePath1 = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\text1.txt";
    private string inputFilePath2 = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\text2.txt";
    private string outputFilePath = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\output3.txt";

    public async Task Run()
    {
        try
        {
            // Перевірка існування файлів
            if (!File.Exists(inputFilePath1))
            {
                throw new FileNotFoundException($"Файл {inputFilePath1} не знайдено.");
            }
            if (!File.Exists(inputFilePath2))
            {
                throw new FileNotFoundException($"Файл {inputFilePath2} не знайдено.");
            }

            // Читання текстів
            string text1 = await File.ReadAllTextAsync(inputFilePath1);
            string text2 = await File.ReadAllTextAsync(inputFilePath2);

            // Розбиття текстів на слова
            string[] words1 = text1.Split(new[] { ' ', '\n', '\r', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
            string[] words2 = text2.Split(new[] { ' ', '\n', '\r', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            // Нормалізація слів (видаляємо регістр для порівняння)
            HashSet<string> words2Set = new HashSet<string>(words2.Select(w => w.Trim().ToLower()), StringComparer.OrdinalIgnoreCase);
            List<string> uniqueWords = new List<string>();

            // Зберігаємо слова з першого тексту, яких немає у другому
            foreach (string word in words1)
            {
                string trimmedWord = word.Trim();
                if (!string.IsNullOrEmpty(trimmedWord) && !words2Set.Contains(trimmedWord.ToLower()))
                {
                    uniqueWords.Add(trimmedWord);
                }
            }

            // Формуємо результат
            string result = string.Join(" ", uniqueWords);
            await File.WriteAllTextAsync(outputFilePath, result);
            Console.WriteLine($"Результат записано у {outputFilePath}");
            Console.WriteLine("Слова, якi є в першому текстi, але вiдсутнi у другому:");
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.ReadKey();
    }
}