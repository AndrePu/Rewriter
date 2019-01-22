using System;
using System.Collections.Generic;
using Rewriter.Entity;

namespace Rewriter.Configuration
{
    internal static class Vocabulary
    {
        public static WordContext db { get; set; }
        public static List<string> words { get; set; } = new List<string>();
    }
}
