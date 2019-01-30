using System;
using System.Collections.Generic;
using System.Threading;

namespace Rewriter.Configuration
{
    public class CorrectWord
    {
        public string Word { get; set; }
        public int Distance { get; set; }

        public CorrectWord(string word, int distance)
        {
            Word = word;
            Distance = distance;
        }
    }
    internal class Vocabulary
    {
        object locked = new object();

        private bool Tuned { get; set; }                                  // tells whether tools(variables) were tuned for correction process
        public List<string> words = new List<string>();
        private List<CorrectWord> avar = new List<CorrectWord>();          // appropriate variants for words correction (DS for word correction process)

        private const int cor_variants = 3;                               // amount of variants to correct the word (Needed for manual editing)
        private const int alength = 10;                               // appropriate Levengshtein distance
        private const double words_part = 0.1;                                // variable for finding amount of threads needed for searching necessary word for correction
        private static int threads_amount;
        private static int wit;                                           //words in every thread                                   
        

        #region Methods

        public void TuneCheckingTools()
        {
            threads_amount = (int)(words.Count * words_part) + 1;
            wit = words.Count / threads_amount;

            Tuned = true;
        }
        public bool Contains(string word)
        {
            return (Algorithm.BinarySearch(words, word.ToLower()) != -1); 
        }
        
        #region Correction word methods
        private void AddCorrectWord(CorrectWord correctWord)
        {
            lock (locked)
            {
                avar.Add(correctWord);
            }
        }
        private void CorrectRange(int range_index, string word_to_correct)
        {
            int begin = range_index * wit;
            int end = ((range_index == threads_amount-1) ? words.Count : (begin + wit));

            for (int i = begin; i < end; i++)
            {
                int dis = Algorithm.DLDistance(words[i], word_to_correct);

                if (dis < alength)
                {
                    AddCorrectWord(new CorrectWord(words[i], dis));
                }
            }
        }
        private void UseThreads(string word_to_correct)
        {
            int index = 0;
            avar.Clear();                                       // Clear all the possible variants for the previous word
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < threads_amount; i++)
            {
                Thread thread = new Thread(() => CorrectRange(index++, word_to_correct));     // creating threads to find appropriate answer 
                threads.Add(thread);
            }

            for (int i = 0; i < threads_amount; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threads_amount; i++)
            {
                threads[i].Join();
            }
        }

        public string CorrectWord(string word_to_correct)
        {
            if (!Tuned)
            {
                throw (new Exception("Variables for checking were not tuned!"));
            }
            bool firstUpper = (word_to_correct[0] >= 'A' && word_to_correct[0] <= 'Z') ? true : false;
            word_to_correct = word_to_correct.ToLower();

            string corrected_word = word_to_correct;            // in case of bad editing distance we return initial word

            UseThreads(word_to_correct);

            Algorithm.QuickSort(avar, 0, avar.Count - 1);
            if (avar.Count != 0)
            {
                corrected_word = avar[0].Word;
            }
            
            if (firstUpper)
                corrected_word = Algorithm.Capitilize(corrected_word);

            return corrected_word;
        }

        /// <summary>
        /// Returns words that are the most suitable for word correction
        /// </summary>
        /// <param name="word_to_correct"></param>
        /// <returns></returns>
        public List<string> VarToCorrectWord(string word_to_correct)
        {
            if (!Tuned)
            {
                throw (new Exception("Variables for checking were not tuned!"));
            }
            bool firstUpper = Algorithm.IsCapitilized(word_to_correct);
            word_to_correct = word_to_correct.ToLower();

            List<string> variants = new List<string>();
            int index = 0;
            avar.Clear();                                       // Clear all the possible variants for the previous word
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < threads_amount; i++)
            {
                Thread thread = new Thread(() => CorrectRange(index++, word_to_correct));     // creating threads to find appropriate answer 
                threads.Add(thread);
            }

            for (int i = 0; i < threads_amount; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threads_amount; i++)
            {
                threads[i].Join();
            }
            
            Algorithm.QuickSort(avar,0, avar.Count - 1);

            for (int i = 0; i < cor_variants && i < avar.Count; i++)
            {
                variants.Add(avar[i].Word);
            }

            if (firstUpper)
            {
                for (int i = 0; i < variants.Count; i++)
                {
                    variants[i] = Algorithm.Capitilize(variants[i]);
                }
            }

            return variants;
        }

        #endregion

        #endregion
    }
}

