using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DAL.EF;
using Library.Entities;

namespace Library.DAL.Repository
{
    public class BookInPublishingHouseRepository : BaseRepository<BookInPublishingHouse> 
    {
        public BookInPublishingHouseRepository(ApplicationContext context) : base(context)
        {
        }

        public List<PublishingHouse> GetBookPublishingHouses(Book book)
        {
            List<BookInPublishingHouse> bookInPublishingHouses = _context.BookInPublishingHouses.Where(b => b.Book.Id == book.Id).ToList();
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>();
            foreach (var item in bookInPublishingHouses)
            {
                publishingHouses.Add(_context.PublishingHouses.Find(item.PublishingHouse));
            }
            return publishingHouses;
        }

        public List<Book> GetPublishingHouseBooks(PublishingHouse publishingHouse)
        {
            List<BookInPublishingHouse> bookInPublishingHouses = _context.BookInPublishingHouses.Where(b => b.PublishingHouse.Id == publishingHouse.Id).ToList();
            List<Book> books = new List<Book>();
            foreach (var item in bookInPublishingHouses)
            {
                books.Add(_context.Books.Find(item.Book));
            }
            return books;
        }
    }
}
