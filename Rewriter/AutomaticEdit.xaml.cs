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
        public AutomaticEdit()
        {
            InitializeComponent();
            SetLanguage();
            SetTextInfo();
        }


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
            Window.Title = "Autochecking..";

            symbAmountPinned_textBlock.Text = "Symbols amount:";
            wordsAmountPinned_textBlock.Text = "Words amount:";
            wordsCheckedPinned_texBlock.Text = "Words checked:";
            wordsCorrectedPinned_textBlock.Text = "Words corrected:";

            timePinned_textBlock.Text = "Time left: ";
            time_textBlock.Text = "1h 2m 32s";
        }

        private void SetRussian()
        {
            Window.Title = "Автопроверка..";

            symbAmountPinned_textBlock.Text = "Количество символов:";
            wordsAmountPinned_textBlock.Text = "Количество слов:";
            wordsCheckedPinned_texBlock.Text = "Слов проверено:";
            wordsCorrectedPinned_textBlock.Text = "Слов исправлено:";

            timePinned_textBlock.Text = "Времени осталось: ";
            time_textBlock.Text = "3ч 23м 1с";
        }

        private void SetUkrainian()
        {
            Window.Title = "Автоперевірка..";

            symbAmountPinned_textBlock.Text = "Кількість символів:";
            wordsAmountPinned_textBlock.Text = "Кількість слів:";
            wordsCheckedPinned_texBlock.Text = "Перевірено слів:";
            wordsCorrectedPinned_textBlock.Text = "Виправлено слів:";

            timePinned_textBlock.Text = "Залишилося часу: ";
            time_textBlock.Text = "3г 23хв 42с";
        }

        #endregion

        /// <summary>
        /// Setting document info to the labels
        /// </summary>
        private void SetTextInfo()
        {
            symbAmount_textBlock.Text = Document.symbolsAmount.ToString();
            wordsAmount_textBlock.Text = Document.wordsAmount.ToString();
            senAmount_textBlock.Text = Document.sentencesAmount.ToString();
            wordsChecked_textBlock.Text = Document.wordsCheckedAmount.ToString();
            wordsCorrected_textBlock.Text = Document.wordsCorrectedAmount.ToString();

            progressBar.Value = Document.wordsCheckedAmount*100 / Document.wordsAmount;
        }
        private void ProcessForm()
        {
            Thread checkText = new Thread(CheckText);

            checkText.Start();
        }
        private void CheckText()
        {
            Thread.Sleep(0);
            AutoCorrectMistakes();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save processed file..";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.DefaultExt = ".txt";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;
                // сохраняем текст в файл
                System.IO.File.WriteAllText(filename, Document.Text);
            }

            this.Close();
        }

        private void AutoCorrectMistakes()
        {
            for (int i = 0; i < Document.sentencesAmount; i++)
            {
                for (int j = 0; j < Document.words[i].Count; j++)
                {
                    if (Vocabulary.Contains(Document.words[i][j]) == false)
                    {
                        string correct_word = Vocabulary.CorrectWord(Document.words[i][j]);
                        Document.Text = Document.Text.Replace(Document.words[i][j], correct_word);
                        Document.wordsCorrectedAmount++;
                    }
                    Document.wordsCheckedAmount++;
                }
                //SetTextInfo();
                //Thread.Sleep(300);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckText();
        }
    }
}
