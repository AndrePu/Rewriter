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
    /// Логика взаимодействия для AutomaticEdit.xaml
    /// </summary>
    public partial class AutomaticEdit : Window
    {
        public AutomaticEdit()
        {
            InitializeComponent();
            SetLanguage();
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


    }
}
