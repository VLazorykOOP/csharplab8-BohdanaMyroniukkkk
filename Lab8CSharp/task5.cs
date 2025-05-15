using System;
using System.IO;
using System.Threading.Tasks;

public class Task5
{
    private string basePath = @"C:\Users\myroniukkk\OneDrive\Робочий стіл\temp";
    private string folder1 = "Myroniuk1";
    private string folder2 = "Myroniuk2";
    private string allFolder = "ALL";

    public async Task Run()
    {
        try
        {
            // Крок 1: Створення папок
            string path1 = Path.Combine(basePath, folder1);
            string path2 = Path.Combine(basePath, folder2);
            Directory.CreateDirectory(path1);
            Directory.CreateDirectory(path2);
            Console.WriteLine($"Створено папки: {path1}, {path2}");

            // Крок 2: Створення файлів у папці Myroniuk1
            string t1Path = Path.Combine(path1, "t1.txt");
            string t2Path = Path.Combine(path1, "t2.txt");
            await File.WriteAllTextAsync(t1Path, "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми");
            await File.WriteAllTextAsync(t2Path, "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ");
            Console.WriteLine($"Створено файли: {t1Path}, {t2Path}");

            // Крок 3: Створення t3.txt у папці Myroniuk2
            string t3Path = Path.Combine(path2, "t3.txt");
            string t1Content = await File.ReadAllTextAsync(t1Path);
            string t2Content = await File.ReadAllTextAsync(t2Path);
            await File.WriteAllTextAsync(t3Path, t1Content + Environment.NewLine + t2Content);
            Console.WriteLine($"Створено файл: {t3Path}");

            // Крок 4: Виведення інформації про створені файли
            Console.WriteLine("\nІнформація про створені файли:");
            DisplayFileInfo(t1Path);
            DisplayFileInfo(t2Path);
            DisplayFileInfo(t3Path);

            // Крок 5: Перенесення t2.txt до Myroniuk2
            string newT2Path = Path.Combine(path2, "t2.txt");
            File.Move(t2Path, newT2Path);
            Console.WriteLine($"\nПеренесено {t2Path} до {newT2Path}");

            // Крок 6: Копіювання t1.txt до Myroniuk2
            string newT1Path = Path.Combine(path2, "t1.txt");
            File.Copy(t1Path, newT1Path);
            Console.WriteLine($"Скопійовано {t1Path} до {newT1Path}");

            // Крок 7: Перейменування Myroniuk2 на ALL і видалення Myroniuk1
            string allPath = Path.Combine(basePath, allFolder);
            Directory.Move(path2, allPath);
            Directory.Delete(path1, true);
            Console.WriteLine($"\nПапку {path2} перейменовано на {allPath}");
            Console.WriteLine($"Папку {path1} видалено");

            // Крок 8: Виведення інформації про файли в папці ALL
            Console.WriteLine("\nІнформація про файли в папці ALL:");
            foreach (string file in Directory.GetFiles(allPath))
            {
                DisplayFileInfo(file);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.ReadKey();
    }

    private void DisplayFileInfo(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        Console.WriteLine($"Файл: {fileInfo.Name}");
        Console.WriteLine($"Розташування: {fileInfo.FullName}");
        Console.WriteLine($"Розмір: {fileInfo.Length} байт");
        Console.WriteLine($"Дата створення: {fileInfo.CreationTime}");
        Console.WriteLine($"Дата останньої зміни: {fileInfo.LastWriteTime}");
        Console.WriteLine();
    }
}