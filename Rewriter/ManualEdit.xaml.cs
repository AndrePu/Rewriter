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
using System.Windows.Forms;
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
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        private string warningTitle = "Warning";
        private string attentionTitle = "Attention";

        private string interrupt_message = "Do you really want to interrupt editing process? Correction process won't be saved!";
        private string warning_message = "You haven't entered variant to use!";
        private string cleanDoc_message = "Your document doesn't have any lexicographical mistakes!";
        private string ownVar_message = "Please,enter your variant..";

        private List<List<string>> corVar = new List<List<string>>();               // correct variants to correct the word
        private int words_processed = 0;                                            // amount of uncorrect words fixed

        public Document document { get; set; }

        private int Current_word { get; set; } = 0; // index of currently analysing word among uncorrect ones in sentence
        private int Current_sentence { get; set; } = 0;   // index currently analysing sentence in text

        public ManualEdit(Document document)
        {
            InitializeComponent();
            SetLanguage();
            SetWindowConfiguration();

            this.document = document;

            if (this.document.Checked)              // nullify result if we used autocheck func
            {
                this.document.Checked = false;
                this.document.EditedText = ProgramOptions.document.Text;
            }
            
            if (this.document.UnWordsAmount == 0)
            {
                if (System.Windows.MessageBox.Show(cleanDoc_message, attentionTitle, MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                    ReturnToMainMenu();
            }
            
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.DefaultExt = ".txt";

            StartFormVariants();

            UpdateSentence();
            ManualCheckMenu();
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
            warningTitle = "Warning";
            attentionTitle = "Attention";

            warning_message = "You haven't entered variant to use!";
            cleanDoc_message = "Your document doesn't have any lexicographical mistakes!";
            ownVar_message = "Please,enter your variant..";
            interrupt_message = "Do you really want to interrupt editing process? Correction process won't be saved!";

            blockName1_textBlock.Text = "Mistakes correcting";
            blockName2_textBlock.Text = "Variants to correct";

            blockName1_textBlock.FontSize = 18;
            blockName2_textBlock.FontSize = 18;

            wordPinned_textBlock.Text = "Word: ";
            sentencePinned.Text = "Sentence: ";

            addWord_button.Content = "Add to vocabulary";
            ownVar_button.Content = "Add own variant";
            ownVar_button.FontSize = 14;

            Window.Title = "Manual edit";

            saveFileDialog1.Title = "Save processed text..";
        }

        private void SetRussian()
        {
            warningTitle = "Предупреждение";
            attentionTitle = "Внимание";

            warning_message = "Вы еще не ввели вариант для правки!";
            cleanDoc_message = "В вашем документе отсутствуют лексографические ошибки!";
            ownVar_message = "Пожалуйста, введите свой вариант..";
            interrupt_message = "Вы действительно хотите приостановить процесс правки? Исправленная часть текста не будет сохранена!";

            blockName1_textBlock.Text = "Исправление ошибок";
            blockName2_textBlock.Text = "Варианты для правки";

            blockName1_textBlock.FontSize = 16;
            blockName2_textBlock.FontSize = 16;

            wordPinned_textBlock.Text = "Слово: ";
            sentencePinned.Text = "Предложение: ";

            addWord_button.Content = "Добавить в словарь";
            ownVar_button.Content = "Добавить свой вариант";
            ownVar_button.FontSize = 13;

            Window.Title = "Проверка вручную";

            saveFileDialog1.Title = "Сохранить обработанный текст..";
        }

        private void SetUkrainian()
        {
            warningTitle = "Попередження";
            attentionTitle = "Увага";

            warning_message = "Ви ще не ввели варіант для виправлення!";
            cleanDoc_message = "У вашому документі відсутні лексографічні помилки!";
            ownVar_message = "Будьте ласкаві, введіть слово..";
            interrupt_message = "Ви дійсно хочете зупинити процесс виправлення? Виправлена частина тексту не буде збережена!";

            blockName1_textBlock.Text = "Виправлення помилок";
            blockName2_textBlock.Text = "Варіанти виправлення";

            blockName1_textBlock.FontSize = 16;
            blockName2_textBlock.FontSize = 16;

            wordPinned_textBlock.Text = "Слово: ";
            sentencePinned.Text = "Речення: ";

            addWord_button.Content = "Додати в словник";
            ownVar_button.Content = "Додати свій варіант";
            ownVar_button.FontSize = 13;

            Window.Title = "Самостійне виправлення";

            saveFileDialog1.Title = "Зберегти оброблений текст..";
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

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (System.Windows.MessageBox.Show(interrupt_message, warningTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ReturnToMainMenu();
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (System.Windows.MessageBox.Show(interrupt_message, warningTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ReturnToMainMenu();
            }
        }

        private void ReturnToMainMenu()
        {
            CheckMenuWindow checkWind = new CheckMenuWindow();
            checkWind.Show();
            this.Close();
        }

        private void ownVar_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((string)ownVar_textBox.Text == ownVar_message)
            {
                ownVar_textBox.Text = String.Empty;
                ownVar_textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void ownVar_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((string)ownVar_textBox.Text == String.Empty)
            {
                ownVar_textBox.Text = ownVar_message;
                ownVar_textBox.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }
        #endregion

        #region Manual checking process
        private void ManualCheckMenu()
        {
            if (words_processed == document.UnWordsAmount)
            {
                /*Saving file..*/
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;

                    System.IO.File.WriteAllText(filename, ProgramOptions.document.EditedText);
                }

                ReturnToMainMenu();
            }
            else if (words_processed == corVar.Count)  // we show next possible variants only if have already got them
            {
                WaitingWindow wait = new WaitingWindow(corVar, words_processed);
                wait.ShowDialog();
                UpdateVariants();
            }
            else
            {
                UpdateVariants();
            }
        }

        private void UpdateVariants()
        {
            firstVar_button.Content = (corVar[words_processed].Count > 0) ? corVar[words_processed][0] : "None";
            secondVar_button.Content = (corVar[words_processed].Count > 1) ? corVar[words_processed][1] : "None";
            thirdVar_button.Content = (corVar[words_processed].Count > 2) ? corVar[words_processed][2] : "None";

             word_textBlock.Text = document.uncorrect_words[Current_sentence][Current_word];

            sentence.Text = document.Sentences[Current_sentence] + '.';
        }

        private void UpdateSentence()
        {

            if (Current_word == document.uncorrect_words[Current_sentence].Count)
            {
                Current_sentence++;
                Current_word = 0;
            }

            while (Current_sentence < document.SentencesAmount && document.uncorrect_words[Current_sentence].Count == 0)
            {
                Current_sentence++;
            }
        }
        
        private void firstVar_button_Click(object sender, RoutedEventArgs e)
        {
            document.EditedText = document.EditedText.Replace(document.uncorrect_words[Current_sentence][Current_word], (string)firstVar_button.Content);

            words_processed++;
            Current_word++;

            UpdateSentence();
            ManualCheckMenu();
        }

        private void secondVar_button_Click(object sender, RoutedEventArgs e)
        {
            document.EditedText = document.EditedText.Replace(document.uncorrect_words[Current_sentence][Current_word], (string)secondVar_button.Content);

            words_processed++;
            Current_word++;

            UpdateSentence();
            ManualCheckMenu();
        }

        private void thirdVar_button_Click(object sender, RoutedEventArgs e)
        {
            document.EditedText = document.EditedText.Replace(document.uncorrect_words[Current_sentence][Current_word], (string)thirdVar_button.Content);

            words_processed++;
            Current_word++;

            UpdateSentence();
            ManualCheckMenu();
        }

        private void addWord_button_Click(object sender, RoutedEventArgs e)
        {
            // Реализовать добавление слова в словарь

            words_processed++;
            Current_word++;

            UpdateSentence();
            ManualCheckMenu();
        }

        private void ownVar_button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)ownVar_textBox.Text == ownVar_message)
            {
                System.Windows.MessageBox.Show(warning_message, warningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                document.EditedText = document.EditedText.Replace(document.uncorrect_words[Current_sentence][Current_word], (string)ownVar_textBox.Text);

                words_processed++;
                Current_word++;

                UpdateSentence();
                ManualCheckMenu();
            }
        }

        #endregion

        #region Forming correct variants to correct the word

        private async void StartFormVariants()
        {
            await Task.Run(() => FormVariants());
        }

        private void FormVariants()
        {
            for (int i = 0; i < document.SentencesAmount; i++)
            {
                for (int j = 0; j < document.uncorrect_words[i].Count; j++)
                {
                    List<string> wordVar = ProgramOptions.vocabulary.VarToCorrectWord(document.uncorrect_words[i][j]);
                    corVar.Add(wordVar);
                }
            }
        }
        #endregion

    }
}
