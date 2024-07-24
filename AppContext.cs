using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAssignment
{
    internal class AppContext : DbContext
    {
        public AppContext()
            : base(@"Data source = .; Initial catalog = Library; Integrated security = true;")
        { }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet <Member >  Members { get; set; }


    }
}
