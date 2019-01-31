using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Rewriter.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Rewriter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool first_run = true;           // indicates whether this form was runned for the first time

        public MainWindow()
        {
            InitializeComponent();
            SetLanguage();
            SetWindowConfiguration();
            
            
            if (first_run) // Loading main vocabulary to program
            {
                first_run = false;
                LoadMainVocabulary();
            }
        }

        #region Loading main vocabulary
        private async void LoadMainVocabulary()
        {
            await Task.Run(() =>            // defining asynchronic operation using lambda expression
            {
                SortedSet<string> sortedWords = new SortedSet<string>();
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Vocabulary data\\google-10000-english-no-swears.txt");

                string line;
                StreamReader sr = new StreamReader(path);

                while ((line = sr.ReadLine()) != null)
                {
                    sortedWords.Add(line);
                }

                ProgramOptions.vocabulary.words = new List<string>(sortedWords);

                ProgramOptions.vocabulary.TuneCheckingTools();                
            });
        }
        #endregion

        #region Window size setting
        private void SetWindowConfiguration()
        {
            this.Width = ProgramOptions.windowConfiguration.Width;
            this.Height = ProgramOptions.windowConfiguration.Height;

            if (first_run)
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                this.Left = ProgramOptions.windowConfiguration.Left;
                this.Top = ProgramOptions.windowConfiguration.Top;
            }

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
            checkText_button.Content = "Start Check Text";
            options_button.Content = "Options";
            info_button.Content = "How to use";
            exit_button.Content = "Exit";
        }

        private void SetRussian()
        {
            checkText_button.Content = "Проверить текст";
            options_button.Content = "Опции";
            info_button.Content = "Справка";
            exit_button.Content = "Выход";
        }

        private void SetUkrainian()
        {
            checkText_button.Content = "Перевірити текст";
            options_button.Content = "Опції";
            info_button.Content = "Довідник";
            exit_button.Content = "Вихід";
        }

        #endregion

        #region Interactivity

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                // Прописать логику закрытия приложения
            }
        }

        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void info_button_Click(object sender, RoutedEventArgs e)
        {
            /* While InfoWindow not done
            InfoWindow info_wind = new InfoWindow();
            info_wind.Show();
            this.Close();
            */
        }

        private void options_button_Click(object sender, RoutedEventArgs e)
        {

            OptionsWindow options_w = new OptionsWindow();
            options_w.Show();
            this.Close();
        }

        private void checkText_button_Click(object sender, RoutedEventArgs e)
        {
            CheckMenuWindow check_w = new CheckMenuWindow();
            check_w.Show();
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
        #endregion
    }
}
