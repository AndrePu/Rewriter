using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Rewriter.Configuration;

namespace Rewriter
{
    public static class Algorithm
    {

        #region Constants for Domerrow-Levenshtain alg
        const int alph_size = 26;
        const int def_lint = 0;
        const int transpose_cost = 1;
        const int delete_cost = 2;
        const int insert_cost = 3;
        const int replace_cost = 2;
        #endregion

        public static int Max(params int[] numbers)
        {
            int max = numbers[0];
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                    max = numbers[i];
            }

            return max;
        }
        public static int Min(params int[] numbers)
        {
            int min = numbers[0];
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] < min)
                    min = numbers[i];
            }

            return min;
        }


        #region QuickSort

        #region QuickSort for CorrectWord class-list
        /// <summary>
        /// Swaps Values by their indeces in List DS
        /// </summary>
        ///
        private static void IndexSwap(List<CorrectWord> list, int ind1, int ind2)
        {
            CorrectWord temp = list[ind1];
            list[ind1] = list[ind2];
            list[ind2] = temp;
        }

        private static int Partition(List<CorrectWord> list, int left, int right)
        {
            int pivot = list[right].Distance;

            int wless = left - 1;       // index of the last lexicographically less word than current one (pivot word)

            for (int i = left; i < right; i++)
            {
                if (pivot > list[i].Distance)
                {
                    wless++;

                    IndexSwap(list, i, wless);
                }
            }

            wless++;

            IndexSwap(list, wless, right);

            return wless;

        }
        
        public static void QuickSort(List<CorrectWord> list, int left, int right)
        {
            if (left < right)
            {
                int pi = Partition(list, left, right);

                QuickSort(list, left, pi - 1);
                QuickSort(list, pi + 1, right);
            }
        }
        #endregion

        #region Quicksort for string-list
        private static void IndexSwap(List<string> list, int ind1, int ind2)
        {
            string temp = list[ind1];
            list[ind1] = list[ind2];
            list[ind2] = temp;
        }

        private static int Partition(List<string> list, int left, int right)
        {
            string pivot = list[right];

            int wless = left - 1;       // index of the last lexicographically less word than current one (pivot word)

            for (int i = left; i < right; i++)
            {
                if (String.Compare(pivot, list[i]) == 1)
                {
                    wless++;

                    IndexSwap(list, i, wless);
                }
            }

            wless++;

            IndexSwap(list, wless, right);

            return wless;

        }

        /// <summary>
        /// Sorts all the words in ascending order
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void QuickSort(List<string> list, int left, int right)
        {
            if (left < right)
            {
                int pi = Partition(list, left, right);

                QuickSort(list, left, pi - 1);
                QuickSort(list, pi + 1, right);
            }
        }
        #endregion  

        #endregion

        public static int BinarySearch(List<string> list, string word_to_find)
        {
            int left = 0;
            int right = list.Count-1;

            while (left <= right)
            {
                int median = left + (right - left) / 2;

                if (list[median] == word_to_find)
                    return median;

                if (String.Compare(word_to_find, list[median]) == 1)
                    left = median + 1;
                else
                    right = median - 1;
            }

            return -1;
        }

        #region Leveshtein-Damerau distance algorithm
        /// <summary>
        /// Damerau-Levenshtain function returns edit distance between two words.
        /// This Distance can be defined by operations:
        /// 1) Insert symbol
        /// 2) Delete symbol
        /// 3) Substract symbol
        /// 4) Transpose two symbols (Damerau extra feature)
        /// More detailed info:
        /// https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance
        /// https://neerc.ifmo.ru/wiki/index.php?title=Задача_о_расстоянии_Дамерау-Левенштейна
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static int DLDistance(string word1, string word2)
        {
            #region Corner cases process
            if (word1 == String.Empty && word2 == String.Empty)
            {
                return 0;
            }
            else if (word2 == String.Empty)
                return word1.Length;
            else if (word1 == String.Empty)
                return word2.Length;
            #endregion

            Dictionary<char, int> lastIndex = new Dictionary<char, int>();                      // Indicates last entrance of each charater in word1 from symbols word1, word2
            List<List<int>> tab_table = new List<List<int>>();          // Tabulation table used for calculation word distance
            int INF = (word1.Length + word2.Length) * Max(insert_cost, delete_cost, replace_cost, transpose_cost);  // const

            #region Initialize priorly DS

            for (int i = 0; i < word1.Length + 1; i++)
            {
                List<int> cur = new List<int>();
                for (int j = 0; j < word2.Length + 1; j++)
                {
                    cur.Add(def_lint);
                }
                tab_table.Add(cur);
            }

            foreach (char letter in (word1 + word2))
            {
                lastIndex[letter] = 0;
            }

            #endregion

            #region Forming induction base

            tab_table[0][0] = INF;
            for (int i = 0; i < word1.Length; i++)
            {
                tab_table[i + 1][1] = i * delete_cost;
                tab_table[i + 1][0] = INF;
            }

            for (int j = 0; j < word2.Length; j++)
            {
                tab_table[1][j + 1] = j * insert_cost;
                tab_table[0][j + 1] = INF;
            }
            #endregion

            #region Tabulation table fill (FINDING MINIMAL COST FOR PREFIXES)
            for (int i = 1; i < word1.Length; i++)
            {
                int last = 0;           // for this iteration this is the index of the last symbol in word2, so word2[last] == word1[i]
                for (int j = 1; j < word2.Length; j++)
                {
                    int i1 = lastIndex[word2[j]];   // last index of the current symbol of word2 in word1
                    int j1 = last;                  // the same for the word2

                    if (word1[i] == word2[j])   // in case of equality symbols of appropriate words, we don't increase distance for following word prefix
                    {
                        tab_table[i + 1][j + 1] = tab_table[i][j];
                        last = j;
                    }
                    else // finding the optimal value (Levenshtain algorithm part)
                    {
                        tab_table[i + 1][j + 1] = Min(tab_table[i][j] + replace_cost, tab_table[i + 1][j] + insert_cost,
                            tab_table[i][j + 1] + delete_cost);
                    }

                    tab_table[i + 1][j + 1] = Min(tab_table[i + 1][j + 1], tab_table[i1][j1] + (i - i1 - 1) * delete_cost + transpose_cost + (j - j1 - 1) * insert_cost);
                }
                lastIndex[word1[i]] = i;
            }
            #endregion

            return tab_table[word1.Length][word2.Length];
        }
        #endregion

        public static string Capitilize(string word)
        {
            if (word == String.Empty || word[0] <= 'a' || word[0] >= 'z')
                return word;

            string wordCapitilized = String.Empty;

            wordCapitilized += (char)(word[0]-32);
            for (int i = 1; i < word.Length; i++)
            {
                wordCapitilized += word[i];
            }

            return wordCapitilized;
        }
    }
}