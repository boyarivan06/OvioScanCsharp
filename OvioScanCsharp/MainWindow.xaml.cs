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

namespace OvioScanCsharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            string path = path_box.Text;
            //string path = "C:/Users/apple/source/repos/OvioScanCsharp/OvioScanCsharp/images/стих.jpg";
            var Ocr = new IronTesseract();
            //Ocr.Language = OcrLanguage.Russian;
            if (file_radio.IsChecked == true && path != String.Empty)
            {
                try
                {
                    using (var Input = new OcrInput(path))
                    {
                        //Input.Deskew();  // use if image not straight
                        //Input.DeNoise(); // use if image contains digital noise

                        var Result = Ocr.Read(Input);
                        // Console.WriteLine("End of Ocr.Read");
                        output_Box.Text = Result.Text; // smth is going on?
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    warningBox.Text = "Файл не найден";
                }
            }
            else 
            {
                warningBox.Text = "Обработка папки пока недоступна";
            }
        }
    }
}
