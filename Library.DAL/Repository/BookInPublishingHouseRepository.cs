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

        public List<PublishingHouse> GetBookPublishingHouses(string Id)
        {
            List<BookInPublishingHouse> bookInPublishingHouses = Context.BookInPublishingHouses.Where(b => b.Book.Id == Id).ToList();
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>();
            foreach (var item in bookInPublishingHouses)
            {
                publishingHouses.Add(Context.PublishingHouses.Find(item.PublishingHouse.Id));
            }
            return publishingHouses;
        }

        public List<Book> GetPublishingHouseBooks(string Id)
        {
            List<BookInPublishingHouse> bookInPublishingHouses = Context.BookInPublishingHouses.Where(b => b.PublishingHouse.Id == Id).ToList();
            List<Book> books = new List<Book>();
            foreach (var item in bookInPublishingHouses)
            {
                books.Add(Context.Books.Find(item.Book.Id));
            }
            return books;
        }
    }
}
