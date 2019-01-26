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
    /// Логика взаимодействия для ManualEdit.xaml
    /// </summary>
    public partial class ManualEdit : Window
    {
        public ManualEdit()
        {
            InitializeComponent();
            SetLanguage();
            SetWindowConfiguration();

           // text_textBox.Text = Document.Text;
            sentence.Text = ProgramOptions.document.Sentences[0] + '.';


            foreach (string w in ProgramOptions.vocabulary.words)
            {
                text_textBox.Text += w + ", ";
            }
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
        // ДОДЕЛАТЬ БЛОК !!!!!!!!!!!!!!!!!!!
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
           
        }

        private void SetRussian()
        {
        }

        private void SetUkrainian()
        {
        }

        #endregion

        #region Interactivity
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

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ( MessageBox.Show("Do you really want to interrupt editing process? Correction process will not be saved!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                CheckMenuWindow checkWind = new CheckMenuWindow();
                checkWind.Show();
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

    }
}
