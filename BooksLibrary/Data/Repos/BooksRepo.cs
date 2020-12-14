using BooksLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Data.Repos
{
    public class BooksRepo : Repository<Book, ApplicationDbContext>
    {
        public BooksRepo(ApplicationDbContext context)
            : base(context) {}
    }
}
