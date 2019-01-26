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

namespace Rewriter
{
    /// <summary>
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
            SetLanguage();
            SetWindowConfiguration();
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
            window.Title = "How to use";
        }

        private void SetRussian()
        {
            window.Title = "Справка";
        }

        private void SetUkrainian()
        {
            window.Title = "Довідник";
        }

        #endregion
        
        #region Interactivity
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ReturnToMainMenu();
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
        #endregion
    }
}
