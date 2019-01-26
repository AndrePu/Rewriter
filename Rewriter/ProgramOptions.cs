using System;
using Rewriter.Configuration;

namespace Rewriter
{

    internal static class ProgramOptions
    {
        public static Document document;
        public static AppLanguage language;
        public static Vocabulary vocabulary;
        public static WindowConfiguration windowConfiguration;

        static ProgramOptions()
        {
            document = new Document();
            language = AppLanguage.English;
            vocabulary = new Vocabulary();
            windowConfiguration = new WindowConfiguration();
        }
    }
}
