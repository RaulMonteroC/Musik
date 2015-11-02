using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musik.Core.Data
{
    public class MusikContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public MusikContext() : base("MusikDb") { }
    }
}
