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
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public CheckMenuWindow()
        {
            InitializeComponent();
            SetLanguage();
            SetWindowConfiguration();

            openFileDialog1.Title = "Open file to work with..";
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog1.DefaultExt = ".txt";
           
        }

        #region Window size setting
        private void SetWindowConfiguration()
        {
            this.Width = WindowConfiguration.Width;
            this.Height = WindowConfiguration.Height;

            this.Left = WindowConfiguration.Left;
            this.Top = WindowConfiguration.Top;

            if (WindowConfiguration.windowState == Rewriter.Configuration.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Maximized;
        }

        #endregion

        #region Setting language for this Window

        private void SetLanguage()
        {
            switch (AppLanguage.language)
            {
                case Rewriter.Configuration.Language.English:
                    SetEnglish();
                    break;
                case Rewriter.Configuration.Language.Russian:
                    SetRussian();
                    break;
                case Rewriter.Configuration.Language.Ukrainian:
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

            uploadInfo_label.Margin = new Thickness(5, 0, 0, 27);
            manualInfo_label.Margin = new Thickness(5, 0, 0, 27);

            document_TextBlock.Text = "Document:";
            fileUploaded_textBlock.Text = Document.Filename;
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

            autoInfo_label.Margin = new Thickness(5,0,0,35);
            manualInfo_label.Margin = new Thickness(5, 0, 0, 35);

            document_TextBlock.Text = "Документ:";
            fileUploaded_textBlock.Text = Document.Filename;
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

            autoInfo_label.Margin = new Thickness(5, 0, 0, 35);
            manualInfo_label.Margin = new Thickness(5, 0, 0, 35);

            document_TextBlock.Text = "Документ:";
            fileUploaded_textBlock.Text = Document.Filename;
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
                WindowConfiguration.windowState = Rewriter.Configuration.WindowState.Maximized;
            }
            else if (this.WindowState == System.Windows.WindowState.Normal)
            {
                WindowConfiguration.windowState = Rewriter.Configuration.WindowState.Normal;
            }
            else
            {
                WindowConfiguration.windowState = Rewriter.Configuration.WindowState.Minimized;
            }

            WindowConfiguration.Width = this.Width;
            WindowConfiguration.Height = this.Height;

            WindowConfiguration.Left = this.Left;
            WindowConfiguration.Top = this.Top;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            WindowConfiguration.Left = this.Left;
            WindowConfiguration.Top = this.Top;
        }
        #endregion
        


        private void auto_rectME_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Document.IsOpened())
            {
                AutomaticEdit auto_edit = new AutomaticEdit();
                auto_edit.Show();
            }
            else
                System.Windows.MessageBox.Show("No file to check was opened!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void manual_rectME_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Document.IsOpened())
            {
                ManualEdit manual_edit = new ManualEdit();
                manual_edit.Show();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("No file to check was opened!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void upload_rectME_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            // Get chosen file
            Document.Filename = openFileDialog1.FileName;
            fileUploaded_textBlock.Text = Document.Filename;

            // Read content of file to string
            Document.Text = System.IO.File.ReadAllText(Document.Filename, Encoding.GetEncoding(1251));


            string clear_text = Document.Text.Replace("\n", "").Replace("\r", ""); // text without \n and \r symbols
            Document.Sentences = clear_text.Remove(clear_text.Length - 1).Split('!', '?', '.');

            Document.FormWordsToCheck();           // Choose words from sentences that needed to be edited

            Document.MakeInfo();                     // Makes information about document
        }
    }
}
