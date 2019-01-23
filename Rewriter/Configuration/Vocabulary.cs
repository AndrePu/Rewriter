using System;
using System.Collections.Generic;
using Rewriter.Entity;

namespace Rewriter.Configuration
{
    internal static class Vocabulary
    {
        public static WordContext db { get; set; }
        public static List<string> words = new List<string>();

        #region Methods

        #region Searching word in vocabulary function
        private static int BinarySearch(string word_to_find)
        {
            int left = 0;
            int right = words.Count;

            while (left <= right)
            {
                int median = left + (right - left) / 2;

                if (words[median] == word_to_find)
                    return median;

                if (String.Compare(word_to_find, words[median]) == 1)
                    left = median + 1;
                else
                    right = median - 1;
            }

            return -1;
        }

        public static bool Contains(string word)
        {
            return (BinarySearch(word.ToLower()) != -1);
        }
        #endregion

        #region Words sorting
        private static void Swap(int ind1, int ind2)
        {
            string temp = words[ind1];
            words[ind1] = words[ind2];
            words[ind2] = temp;
        }

        private static int Partition(int left, int right)
        {
            string pivot = words[right];

            int wless = left - 1;       // index of the last lexicographically less word than current one (pivot word)

            for (int i = left; i < right; i++)
            {
                if (String.Compare(pivot,words[i]) == 1)
                {
                    wless++;
                    
                    Swap(i, wless);
                }
            }

            wless++;

            Swap(wless, right);

            return wless;

        }
        /// <summary>
        /// Sorts all the words in ascending order
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void QuickSort(int left, int right)
        {
            if (left < right)
            {
                int pi = Partition(left, right);

                QuickSort(left, pi - 1);
                QuickSort(pi + 1, right);
            }
        }
        #endregion

        #region
        public static string CorrectWord(string word)
        {
            string corrected_word = "Mima";

            return corrected_word;
        }

        #endregion
        #endregion
    }
}
