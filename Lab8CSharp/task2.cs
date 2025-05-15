using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Task2
{
    private string inputFilePath = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\input2.txt";
    private string outputFilePath = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\output2.txt";

    public async Task Run()
    {
        try
        {
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException("Вхiдний файл не знайдено.");
            }

            string text = await File.ReadAllTextAsync(inputFilePath);
            string[] words = Regex.Split(text, @"\s+");

            var processedWords = new List<string>();
            foreach (string word in words)
            {
                string trimmedWord = word.Trim(new char[] { '.', ',', '!', '?', ';', ':' });
                if (string.IsNullOrEmpty(trimmedWord)) continue;

                if (trimmedWord.EndsWith("re", StringComparison.OrdinalIgnoreCase) ||
                    trimmedWord.EndsWith("nd", StringComparison.OrdinalIgnoreCase) ||
                    trimmedWord.EndsWith("less", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string processedWord = trimmedWord.StartsWith("to", StringComparison.OrdinalIgnoreCase)
                    ? "at" + trimmedWord.Substring(2)
                    : trimmedWord;

                processedWords.Add(processedWord);
            }

            string result = string.Join(" ", processedWords);
            foreach (char c in new[] { '.', ',', '!', '?', ';', ':' })
            {
                result = Regex.Replace(result, $@"\s+{c}", c.ToString());
            }

            await File.WriteAllTextAsync(outputFilePath, result);
            Console.WriteLine($"Результат записано у {outputFilePath}");
            Console.WriteLine("Оброблений текст:");
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.ReadKey();
    }
}