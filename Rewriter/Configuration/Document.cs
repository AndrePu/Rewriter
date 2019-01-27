using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Rewriter.Configuration
{
    public class Document : INotifyPropertyChanged  // class will be connected to properties of control elements
    {
        private string filename = null;             // shows current path and name of document
        private string text = null;
        private string edited_text = null;
        private int wordsAmount;
        private int symbolsAmount = 0;
        private int sentencesAmount;
        private int wordsCheckedAmount;
        private int wordsCorrectedAmount = 10;
        private int textChecked;                    // percents of how much of text were checked

        public List<List<string>> words = new List<List<string>>();          // all the words in document
        

        #region Autopropeties of class
        public string Filename
        {
            get
            {
                if (filename == null)
                    return "None";
                else
                    return filename;
            }
            set
            {
                filename = value;
                OnPropertyChanged("Filename");
            }
        }       

        public string Text                  // content of document
        {
            get
            {
                return text;
            }
            set
            { 
                text = value;
                EditedText = text;
                OnPropertyChanged("Text");
            }
        }         

        public string EditedText
        {
            get
            {
                return edited_text;
            }
            set
            {
                edited_text = text;
            }
        }
        public int WordsAmount
        {
            get
            {
                return wordsAmount;
            }
            set
            {
                wordsAmount = value;
                OnPropertyChanged("WordsAmount");
            }
        }

        public int SymbolsAmount
        {
            get
            {
                return symbolsAmount;
            }
            set
            {
                symbolsAmount = value;
                OnPropertyChanged("SymbolsAmount");
            }
        }

        public int SentencesAmount
        {
            get
            {
                return sentencesAmount;
            }
            set
            {
                sentencesAmount = value;
                OnPropertyChanged("SentencesAmount");
            }
        }

        public int WordsCheckedAmount
        {
            get
            {
                return wordsCheckedAmount;
            }
            set
            {
                wordsCheckedAmount = value;
                OnPropertyChanged("WordsCheckedAmount");

                TextChecked = wordsCheckedAmount * 100 / WordsAmount;   // making dependent textChecked variable from current one
            }
        }

        public int WordsCorrectedAmount
        {
            get
            {
                return wordsCorrectedAmount;
            }
            set
            {
                wordsCorrectedAmount = value;
                OnPropertyChanged("WordsCorrectedAmount");
            }
        }

        public int TextChecked
        {
            get
            {
                return textChecked;
            }
            set
            {
                textChecked = value;
                OnPropertyChanged("TextChecked");
            }
        }
        public bool Checked { get; set; } = false;

        public string[] Sentences { get; set; }

        #region Variables for manual correcting
        public int Current_word { get; set; } = 0; // index of currently analysing word among uncorrect ones in sentence
        public int Current_sentence { get; set; } = 0;   // index currently analysing sentence in text
        #endregion

        #endregion


        #region Methods
        
        public void FormWordsToCheck()
        {
            for (int i = 0; i < Sentences.Length; i++)
            {
                List<string> words_inCurSen = new List<string>();        // words in current sentence
                string[] cur_sentence = Sentences[i].Split(' ');

                for (int j = 0; j < cur_sentence.Length; j++)
                {
                        words_inCurSen.Add(cur_sentence[j]);
                }

                words.Add(words_inCurSen);
            }
        }

        public void GetInfo()
        {
            sentencesAmount = Sentences.Length;

            symbolsAmount = Text.Length;
            
            for (int i = 0;  i  < words.Count; i++)
            {
                wordsAmount += words[i].Count;
            }
        }

        /// <summary>
        /// Tells whether document to check and edit is loaded in program
        /// </summary>
        /// <returns></returns>
        public bool IsOpened()
        {
            return filename != null;
        }

        // Updating properties of control elements after binding properties were changed
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
