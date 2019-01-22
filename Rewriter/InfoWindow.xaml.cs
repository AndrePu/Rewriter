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
