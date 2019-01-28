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
using System.Threading;
using System.Windows.Forms;

namespace Rewriter
{
    /// <summary>
    /// Логика взаимодействия для AutomaticEdit.xaml
    /// </summary>
    public partial class AutomaticEdit : Window
    {
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        public Document document { get; set; }      // Property that indicates info of document
        
        public AutomaticEdit(Document document)
        {
            InitializeComponent();

            this.document = document;
            this.document.WordsCheckedAmount = 0;
            this.document.WordsCorrectedAmount = 0;

            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.DefaultExt = ".txt";

            SetLanguage();
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
            Window.Title = "Autochecking..";

            symbAmountPinned_textBlock.Text = "Symbols amount:";
            wordsAmountPinned_textBlock.Text = "Words amount:";
            wordsCheckedPinned_texBlock.Text = "Words checked:";
            wordsCorrectedPinned_textBlock.Text = "Words corrected:";

            timePinned_textBlock.Text = "Time left: ";
            time_textBlock.Text = "1h 2m 32s";

            saveFileDialog1.Title = "Save processed file..";
        }

        private void SetRussian()
        {
            Window.Title = "Автопроверка..";

            symbAmountPinned_textBlock.Text = "Количество символов:";
            wordsAmountPinned_textBlock.Text = "Количество слов:";
            wordsCheckedPinned_texBlock.Text = "Слов проверено:";
            wordsCorrectedPinned_textBlock.Text = "Слов исправлено:";
            senAmountPinned_textBlock.Text = "Количество предложений:";

            timePinned_textBlock.Text = "Времени осталось: ";
            time_textBlock.Text = "3ч 23м 1с";

            saveFileDialog1.Title = "Сохранить обработанный файл..";
        }

        private void SetUkrainian()
        {
            Window.Title = "Автоперевірка..";

            symbAmountPinned_textBlock.Text = "Кількість символів:";
            wordsAmountPinned_textBlock.Text = "Кількість слів:";
            wordsCheckedPinned_texBlock.Text = "Перевірено слів:";
            wordsCorrectedPinned_textBlock.Text = "Виправлено слів:";
            senAmountPinned_textBlock.Text = "Кількість речень:";

            timePinned_textBlock.Text = "Залишилося часу: ";
            time_textBlock.Text = "3г 23хв 42с";

            saveFileDialog1.Title = "Зберегти оброблений файл..";
        }

        #endregion

        #region Checking text process
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckText();
        }
        
        private async void CheckText()
        {
            if (document.Checked == false)  // check text if only we haven't done it yet
                await Task.Run(() => AutoCorrectMistakes());

            /*Saving file..*/
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                
                System.IO.File.WriteAllText(filename, ProgramOptions.document.EditedText);
            }

            this.Close();
        }

        private void AutoCorrectMistakes()
        {
            for (int i = 0; i < ProgramOptions.document.SentencesAmount; i++)
            {
                for (int j = 0; j < ProgramOptions.document.words[i].Count; j++)
                {
                    if (ProgramOptions.vocabulary.Contains(ProgramOptions.document.words[i][j]) == false)
                    {
                        string correct_word = ProgramOptions.vocabulary.CorrectWord(ProgramOptions.document.words[i][j]);
                        ProgramOptions.document.Text = ProgramOptions.document.EditedText.Replace(ProgramOptions.document.words[i][j], correct_word);
                        ProgramOptions.document.WordsCorrectedAmount++;
                    }
                    
                    ProgramOptions.document.WordsCheckedAmount++;
                    Thread.Sleep(10);                                         // TESTING LINE | DELETE!!!!!!              
                }
            }
            ProgramOptions.document.Checked = true;
        }
        #endregion
    }
}
