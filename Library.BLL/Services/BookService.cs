using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;

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
            publicationRepository.Insert(publication);

            Book book = new Book //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Author = item.Author,
                Name = item.Name,
                YearOfPublishing = item.YearOfPublishing,
                PublicationId = publication.Id
            };
            bookRepository.Insert(book);

            SaveChanges();
        }

        public DetailsBookViewModel GetByIdDetails(string id)
        {
            Book book = bookRepository.GetByID(id);
            return new DetailsBookViewModel //TODO: Automaper
            {
                Author = book.Author,
                Name = book.Name,
                YearOfPublishing = book.YearOfPublishing
            };
        }

        public IndexBookViewModel GetAll()
        {
            var bookIndexVM = new IndexBookViewModel();
            bookIndexVM.books = bookRepository.Get().Select(x => new BookViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                YearOfPublishing = x.YearOfPublishing,
            }).ToList();
            return bookIndexVM;
        }

        public EditBookViewModel GetByIdEdit(string id)
        {
            Book book = bookRepository.GetByID(id);
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
            Book book = bookRepository.GetByID(id);
            book.Name = bookViewModel.Name;
            book.YearOfPublishing = bookViewModel.YearOfPublishing;
            book.Author = bookViewModel.Author;
            bookRepository.Update(book);

            Publication publication = publicationRepository.GetByID(book.PublicationId);
            publication.Name = bookViewModel.Name;
            publicationRepository.Update(publication);

            SaveChanges();
        }

        public DeleteBookViewModel GetByIdDelete(string id)
        {
            Book book = bookRepository.GetByID(id);
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
                Book book = bookRepository.GetByID(id);

                publicationRepository.Delete(book.PublicationId);

                bookRepository.Delete(id);

                SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private void SaveChanges()
        {
            bookRepository.Save();
            publicationRepository.Save();
        }
    }
}
