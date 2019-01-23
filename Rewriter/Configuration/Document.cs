using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewriter.Configuration
{
    internal static class Document
    {
        private static string filename = null;      // shows current path and name of document
        private static string text = null;

        public static List<List<string>> words = new List<List<string>>();          // list of words in sentences that needs in editing


        #region Autopropeties of class
        public static string Filename
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
            }
        }       

        public static string Text                  // content of document
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }         

        public static string[] Sentences { get; set; }
        
        public static int wordsAmount { get; set; } = 0;

        public static int symbolsAmount { get; set; } = 0;

        public static int sentencesAmount { get; set; } = 0;
        public static int wordsCheckedAmount { get; set; } = 0;

        public static int wordsCorrectedAmount { get; set; } = 0;

        #region Variables for manual correcting
        public static int current_word { get; set; } = 0; // index of currently analysing word among uncorrect ones in sentence
        public static int current_sentence { get; set; } = 0;   // index currently analysing sentence in text
        #endregion

        #endregion


        #region Methods
        
        public static void FormWordsToCheck()
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

        public static void MakeInfo()
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
        public static bool IsOpened()
        {
            return filename != null;
        }

        #endregion
    }
}
