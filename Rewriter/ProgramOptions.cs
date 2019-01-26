using System;
using Rewriter.Configuration;

namespace Rewriter
{

    internal static class ProgramOptions
    {
        public static Document document;

        static ProgramOptions()
        {
            document = new Document();
        }
    }
}
