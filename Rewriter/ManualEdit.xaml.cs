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


            foreach (string w in Vocabulary.words)
            {
                text_textBox.Text += w + ", ";
            }
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
        // ДОДЕЛАТЬ БЛОК !!!!!!!!!!!!!!!!!!!
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
