using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;



namespace CodeToDatabase1
{
    public class GradebookContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

    }
}
