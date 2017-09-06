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

            Publication publication = new Publication
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                Type = "Book"
            };
            string publicationId = publicationRepository.Create(publication);

            Book book = new Book //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Author = item.Author,
                Name = item.Name,
                YearOfPublishing = item.YearOfPublishing,
                PublicationId = publicationId
            };
            bookRepository.Create(book);
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

        public EditBookViewModel GetByIdEdit(string id)
        {
            Book book = bookRepository.GetById(id);
            return new EditBookViewModel //TODO: Automaper
            {
                Author = book.Author,
                Name = book.Name,
                YearOfPublishing = book.YearOfPublishing
            };
        }
        
        public void Edit(string id, EditBookViewModel bookViewModel)
        {
            //TODO: validation
            Book book = bookRepository.GetById(id);
            book.Name = bookViewModel.Name;
            book.YearOfPublishing = bookViewModel.YearOfPublishing;
            book.Author = bookViewModel.Author;
            bookRepository.Update(book);

            Publication publication = publicationRepository.GetById(book.PublicationId);
            publication.Name = bookViewModel.Name;
            publicationRepository.Update(publication);
        }

        public DeleteBookViewModel GetByIdDelete(string id)
        {
            Book book = bookRepository.GetById(id);
            return new DeleteBookViewModel
            {
                Author = book.Author,
                Name = book.Name,
                YearOfPublishing = book.YearOfPublishing
            };
        }

        public void Delete(string id)
        {
            try
            {
                Book book = bookRepository.GetById(id);

                publicationRepository.Delete(book.PublicationId);

                bookRepository.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
