using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewriter.Configuration
{
    enum Language
    {
        English,
        Russian,
        Ukrainian
    }
    internal static class AppLanguage   // sets the language of app
    {
        public static Language language = Language.English;      // English by default
    }
}
