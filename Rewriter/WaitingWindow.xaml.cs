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
using System.Threading;
using Rewriter.Configuration;

namespace Rewriter
{
    /// <summary>
    /// Логика взаимодействия для WaitingWindow.xaml
    /// </summary>
    public partial class WaitingWindow : Window
    {
        private List<List<string>> corVar;
        private int words_processed;
        public WaitingWindow(List<List<string>> corVar, int words_processed)
        {
            InitializeComponent();
            SetLanguage();

            this.corVar = corVar;
            this.words_processed = words_processed;

            WaitingProcess();
        }


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

            textblock.Text = "Please, wait a second..";
        }

        private void SetRussian()
        {
            textblock.Text = "Пожалуйста, подождите немного..";
        }

        private void SetUkrainian()
        {
            textblock.Text = "Будь ласка, зачекайте трохи..";
        }

        #endregion
        private async void WaitingProcess()
        {
            await Task.Run(() => Wait());

            this.Close();
        }
        private void Wait()
        {
            while (corVar.Count == words_processed)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
