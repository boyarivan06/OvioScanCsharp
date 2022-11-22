using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IronOcr;
using System.IO;

namespace OvioScanCsharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IronTesseract Ocr = new IronTesseract();

        private void Log(string text)
        {
            File.AppendAllText("logs.txt", $"{text} - {DateTime.Now}");
            File.AppendAllText("logs.txt", Environment.NewLine);
        }

        public MainWindow()
        {
            Closing += (c, e) =>  Log("window closing"); 
            if (!File.Exists("logs.txt")) File.CreateText("logs.txt");
            Log("app starts");
            InitializeComponent();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            string path = path_box.Text;
            Log("runButton clicked");
            //string path = "C:/Users/apple/source/repos/OvioScanCsharp/OvioScanCsharp/images/poem.jpg";
            //Ocr.Language = OcrLanguage.Russian;
            if (file_radio.IsChecked == true && path != String.Empty)
            {
                try
                {
                    Log($"try to create OcrInput from the file {path}");
                    using (var Input = new OcrInput(path))
                    {
                        //Input.Deskew();  // use if image not straight
                        //Input.DeNoise(); // use if image contains digital noise

                        Log($"try to read the OcrInput");
                        string Result = "we can't read this text";
                        Result = Ocr.Read(Input).Text;
                        Log($"end of Ocr.Read");
                        output_Box.Text = Result; // smth is going on?
                    }
                }
                catch (FileNotFoundException)
                {
                    warningBox.Text = "Файл не найден";
                    Log("file not found");
                }
                catch (IronOcr.Exceptions.IronOcrProductException)
                {
                    warningBox.Text = "Файл не является корректным";
                    Log("file isn't correct");
                }
            }
            else 
            {
                warningBox.Text = "Обработка папки пока недоступна";
            }
        }
    }
}
