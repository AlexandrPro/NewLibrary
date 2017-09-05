using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.Book;
using System;
using System.Collections.Generic;

namespace Library.BLL.Services
{
    public class BookService 
    {
        ApplicationContext db;
        BookRepository bookRepository;
        PublicationRepository publicationRepository;

        public BookService()
        {
            db = new ApplicationContext();
            bookRepository = new BookRepository(db);
            publicationRepository = new PublicationRepository(db);
        }

        public void Create(CreateBookViewModel item)
        {
            //TODO: validation
            Book book = new Book //TODO: automaper
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Author = item.Author,
                Name = item.Name,
                YearOfPublishing = item.YearOfPublishing
            };
            bookRepository.Create(book);

            Publication publication = new Publication
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Name = item.Name,
                Type = "Book"
            };
            publicationRepository.Create(publication);
        }

        public IndexBookViewModel GetAll()
        {
            IEnumerable<Book> books = bookRepository.GetAll();
            IndexBookViewModel bookVM = new IndexBookViewModel();
            bookVM.books = new List<BookViewModel>();
            foreach (Book item in books) //TODO: automaper
            {
                bookVM.books.Add(new BookViewModel
                {
                    Id = item.Id,
                    Author = item.Author,
                    Name = item.Name,
                    YearOfPublishing = item.YearOfPublishing
                });
            }

            return bookVM;
        }
    }
}
