using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8CSharp
{
    public partial class Form1 : Form
    {
        private TextBox inputFilePathBox;
        private TextBox outputFilePathBox;
        private Button runButton;

        public Form1()
        {
            InitializeComponent();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            string inputPath = inputFilePathBox.Text.Trim();
            string outputPath = outputFilePathBox.Text.Trim();

            if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
            {
                MessageBox.Show("Будь ласка, вкажіть шляхи до вхідного та вихідного файлів.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Lab8T2 task = new Lab8T2(inputPath, outputPath);
            task.Run(this);
        }
    }

    public class Lab8T2
    {
        private string inputFilePath;
        private string outputFilePath;

        public Lab8T2(string inputPath, string outputPath)
        {
            this.inputFilePath = inputPath;
            this.outputFilePath = outputPath;
        }

        public async void Run(Form form)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!File.Exists(inputFilePath))
                    {
                        form.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show($"Файл {inputFilePath} не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                        return;
                    }

                    string[] lines = File.ReadAllLines(inputFilePath);
                    string pattern = @"-?\d+\.\d+\s*,\s*-?\d+\.\d+";
                    int coordinateCount = 0;
                    string modifiedText = "";

                    foreach (string line in lines)
                    {
                        MatchCollection matches = Regex.Matches(line, pattern);
                        coordinateCount += matches.Count;
                        modifiedText += Regex.Replace(line, pattern, "***") + Environment.NewLine;
                    }

                    File.WriteAllText(outputPath, modifiedText);
                    form.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show($"Знайдено {coordinateCount} координат. Результати записано у {outputFilePath}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                catch (Exception ex)
                {
                    form.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            });
        }
    }
}