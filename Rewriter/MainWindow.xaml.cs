using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Rewriter.Configuration;
using Rewriter.Entity;
using System.Data.Entity;
using System.Threading;

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
                Thread vocLoad = new Thread(LoadMainVocabulary);
                vocLoad.Start();
            }
        }

        #region Loading main vocabulary
        private void LoadMainVocabulary()
        {
            Vocabulary.db = new WordContext();

            Vocabulary.db.words.Load();

            foreach (Words word in Vocabulary.db.words) // loading all the words to the comfortable data structure to work
            {
                Vocabulary.words.Add(word.Word);
            }
        }
        #endregion

        #region Window size setting
        private void SetWindowConfiguration()
        {
            this.Width = WindowConfiguration.Width;
            this.Height = WindowConfiguration.Height;

            if (first_run)
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                first_run = false;
            }
            else
            {
                this.Left = WindowConfiguration.Left;
                this.Top = WindowConfiguration.Top;
            }

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
            InfoWindow info_wind = new InfoWindow();
            info_wind.Show();
            this.Close();
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
    }
}
