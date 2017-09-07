using Library.DAL.EF;
using Library.Entities;
using System.Data.Entity;

namespace Library.DAL.Repository
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
