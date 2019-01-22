using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewriter.Configuration
{
    internal static class Document
    {
        private static string name = null;

        #region Autopropeties of class
        public static string Name
        {
            get
            {
                if (name == null)
                    return "None";
                else
                    return name;
            }
            set
            {
                name = value;
            }
        }
        
        public static int wordsAmount { get; set; } = 0;

        public static int symbolsAmount { get; set; } = 0;

        public static int wordsCheckedAmount { get; set; } = 0;

        public static int wordsCorrectedAmount { get; set; } = 0;
        #endregion

        
        /// <summary>
        /// Tells whether document to check and edit is loaded in program
        /// </summary>
        /// <returns></returns>
        public static bool IsOpened()
        {
            return name != null;
        }
    }
}
