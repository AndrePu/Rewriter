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
using System.Windows.Shapes;
using Rewriter.Configuration;
using System.Windows.Forms;

namespace Rewriter
{
    /// <summary>
    /// Логика взаимодействия для CheckWindow.xaml
    /// </summary>
    public partial class CheckMenuWindow : Window
    {
        private string warningTitle = String.Empty;
        private string warningMessage = String.Empty;

        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public CheckMenuWindow()
        {
            InitializeComponent();
            SetLanguage();
            SetWindowConfiguration();
            
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog1.DefaultExt = ".txt";
           
        }

        #region Window size setting
        private void SetWindowConfiguration()
        {
            this.Width = ProgramOptions.windowConfiguration.Width;
            this.Height = ProgramOptions.windowConfiguration.Height;

            this.Left = ProgramOptions.windowConfiguration.Left;
            this.Top = ProgramOptions.windowConfiguration.Top;

            if (ProgramOptions.windowConfiguration.windowState == Rewriter.Configuration.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Maximized;
        }

        #endregion

        #region Setting language for this Window

        private void SetLanguage()
        {
            switch (ProgramOptions.language)
            {
                case AppLanguage.English:
                    SetEnglish();
                    break;
                case AppLanguage.Russian:
                    SetRussian();
                    break;
                case AppLanguage.Ukrainian:
                    SetUkrainian();
                    break;
                default:
                    SetEnglish();
                    break;
            }

        }

        private void SetEnglish()
        {
            window.Title = "Checking Text";

            upload_textBlock.Text = "Upload file";
            auto_textBlock.Text = "Automatic edit";
            manual_textBlock.Text = "Manual edit";

            upload_textBlock.FontSize = 14;
            auto_textBlock.FontSize = 14;
            manual_textBlock.FontSize = 14;

            uploadInfo_label.Content = "Upload text file in order to check and correct it";
            autoInfo_label.Content = "Correct mistakes automatically";
            manualInfo_label.Content = "Control correctness of every word";

            autoInfo_label.Margin = new Thickness(5, 0, 0, 27);
            manualInfo_label.Margin = new Thickness(5, 0, 0, 27);

            document_TextBlock.Text = "Document:";
            fileUploaded_textBlock.Text = ProgramOptions.document.Filename;

            openFileDialog1.Title = "Open file to work with..";

            warningTitle = "Warning";
            warningMessage = "No file to check was opened!";
        }

        private void SetRussian()
        {
            window.Title = "Проверка текста";

            upload_textBlock.Text = "Загрузить файл";
            auto_textBlock.Text = "Автоматическая проверка";
            manual_textBlock.Text = "Проверка вручную";

            upload_textBlock.FontSize = 13;
            auto_textBlock.FontSize = 13;
            manual_textBlock.FontSize = 13;

            uploadInfo_label.Content = "Загрузить файл для последующей проверки";
            autoInfo_label.Content = "Автоматически исправить ошибки в файле";
            manualInfo_label.Content = "Контроль каждого исправления";

            autoInfo_label.Margin = new Thickness(5,0,0,38);
            manualInfo_label.Margin = new Thickness(5, 0, 0, 38);

            document_TextBlock.Text = "Документ:";
            fileUploaded_textBlock.Text = ProgramOptions.document.Filename;

            openFileDialog1.Title = "Открыть файл для исправления..";

            warningTitle = "Предупреждение";
            warningMessage = "Файл для проверки еще не был загружен!";
        }

        private void SetUkrainian()
        {
            window.Title = "Перевірка тексту";

            upload_textBlock.Text = "Завантажити файл";
            auto_textBlock.Text = "Автоматичне виправлення";
            manual_textBlock.Text = "Самостійне виправлення";

            upload_textBlock.FontSize = 13;
            auto_textBlock.FontSize = 13;
            manual_textBlock.FontSize = 13;

            uploadInfo_label.Content = "Завантажити файл для подальшого аналізу";
            autoInfo_label.Content = "Корегувати помилки у тексті автоматично";
            manualInfo_label.Content = "Контроль кожного виправлення";

            autoInfo_label.Margin = new Thickness(5, 0, 0, 38);
            manualInfo_label.Margin = new Thickness(5, 0, 0, 38);

            document_TextBlock.Text = "Документ:";
            fileUploaded_textBlock.Text = ProgramOptions.document.Filename;


            openFileDialog1.Title = "Відкрити файл для виправлення..";

            warningTitle = "Попередження";
            warningMessage = "Файл для перевірки ще не був завантажений!";
        }

        #endregion

        #region Interactivity
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                // REWRITE logic here
                ReturnToMainMenu();
            }
        }
        
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // REWRITE logic here
            ReturnToMainMenu();
        }

        private void ReturnToMainMenu()
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                ProgramOptions.windowConfiguration.windowState = Rewriter.Configuration.WindowState.Maximized;
            }
            else if (this.WindowState == System.Windows.WindowState.Normal)
            {
                ProgramOptions.windowConfiguration.windowState = Rewriter.Configuration.WindowState.Normal;
            }
            else
            {
                ProgramOptions.windowConfiguration.windowState = Rewriter.Configuration.WindowState.Minimized;
            }

            ProgramOptions.windowConfiguration.Width = this.Width;
            ProgramOptions.windowConfiguration.Height = this.Height;

            ProgramOptions.windowConfiguration.Left = this.Left;
            ProgramOptions.windowConfiguration.Top = this.Top;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            ProgramOptions.windowConfiguration.Left = this.Left;
            ProgramOptions.windowConfiguration.Top = this.Top;
        }


        private void auto_rectME_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ProgramOptions.document.IsOpened())
            {
                AutomaticEdit auto_edit = new AutomaticEdit(ProgramOptions.document);
                auto_edit.ShowDialog();
            }
            else
                System.Windows.MessageBox.Show(warningMessage, warningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void manual_rectME_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ProgramOptions.document.IsOpened())
            {
                ManualEdit manual_edit = new ManualEdit(ProgramOptions.document);
                manual_edit.Show();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show(warningMessage, warningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void upload_rectME_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            // Get chosen file
            ProgramOptions.document = new Document();
            ProgramOptions.document.Filename = openFileDialog1.FileName;
            fileUploaded_textBlock.Text = ProgramOptions.document.Filename;

            // Read content of file to string
            ProgramOptions.document.Text = System.IO.File.ReadAllText(ProgramOptions.document.Filename, Encoding.GetEncoding(1251));


            string clear_text = ProgramOptions.document.Text.Replace("\n", "").Replace("\r", ""); // text without \n and \r symbols

            string[] sentences = clear_text.Remove(clear_text.Length - 1).Replace(",", "").Split('!', '?', '.');

            foreach (string sentence in sentences)
            {
                if (sentence != String.Empty)
                {
                    ProgramOptions.document.Sentences.Add(sentence);
                }
            }

            ProgramOptions.document.FormWordsToCheck();           // Choose words from sentences that needed to be edited

            ProgramOptions.document.GetInfo();                     // Makes information about document
        }
        #endregion
    }
}
