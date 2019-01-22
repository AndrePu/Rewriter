using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Rewriter.Entity
{
    internal class WordContext : DbContext
    {
        public WordContext() : base("DefaultConnection") { }

        public DbSet<Words> words { get; set; }
    }
}
