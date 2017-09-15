using Library.DataAccess.EF;
using Library.Entities;
using System.Data.Entity;

namespace Library.DataAccess.Repository
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
