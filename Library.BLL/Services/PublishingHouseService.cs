using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.PublishingHouse;
using System;
using System.Collections.Generic;

namespace Library.BLL.Services
{
    public class PublishingHouseService
    {
        ApplicationContext db;
        PublishingHouseRepository publishingHouseRepository;
        PublicationRepository publicationRepository;
        BookInPublishingHouseRepository bookInPublishingHouseRepository;
        BookRepository bookRepository;

        public PublishingHouseService()
        {
            db = new ApplicationContext();
            publishingHouseRepository = new PublishingHouseRepository(db);
            publicationRepository = new PublicationRepository(db);
            bookInPublishingHouseRepository = new BookInPublishingHouseRepository(db);
            bookRepository = new BookRepository(db);
        }

        public void Create(CreatePublishingHouseViewModel publishingHouseViewModel)
        {
            //TODO: validation

            Publication publication = new Publication
            {
                CreationDate = DateTime.Now,
                Name = publishingHouseViewModel.Name,
                Type = "PublishingHouse"
            };
            string publicationId = publicationRepository.Create(publication);

            PublishingHouse publishingHouse = new PublishingHouse //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = publishingHouseViewModel.Name,
                Address = publishingHouseViewModel.Address,
            };
            publishingHouseRepository.Create(publishingHouse);

            if (publishingHouseViewModel.BookIds != null)
            {
                foreach (var item in publishingHouseViewModel.BookIds)
                {
                    bookInPublishingHouseRepository.Create(new BookInPublishingHouse()
                    {
                        Book = bookRepository.GetById(item),
                        PublishingHouse = publishingHouse,
                    });
                }
            }
        }

        public BookListViewModel GetAllBooks()
        {
            IEnumerable<Book> books = bookRepository.GetAll();
            BookListViewModel bookListViewModels = new BookListViewModel();
            bookListViewModels.Books = new List<BookViewModel>();
            foreach (var item in books)
            {
                bookListViewModels.Books.Add(new BookViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return bookListViewModels;
        }

        public IndexPublishingHouseViewModel GetAll()
        {
            IEnumerable<PublishingHouse> publishingHouses = publishingHouseRepository.GetAll();
            IndexPublishingHouseViewModel publishingHouseVM = new IndexPublishingHouseViewModel();
            publishingHouseVM.publishingHouses = new List<PublishingHouseViewModel>();
            foreach (PublishingHouse item in publishingHouses) //TODO: automaper
            {
                publishingHouseVM.publishingHouses.Add(new PublishingHouseViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    Books = GetPublishingHouseBooksToString(item),
                });
            }

            return publishingHouseVM;
        }

        private string GetPublishingHouseBooksToString(PublishingHouse publishingHouse)
        {
            List<Book> books = bookInPublishingHouseRepository.GetPublishingHouseBooks(publishingHouse);
            string booksString = "";
            foreach (var item in books)
            {
                booksString += item.Name + ", ";
            }
            if(booksString != "")
                booksString = booksString.Remove(booksString.Length - 2, 2);

            return booksString;
        }

        public EditPublishingHouseViewModel GetByIdEdit(string id)
        {
            PublishingHouse publishingHouse = publishingHouseRepository.GetById(id);
            return new EditPublishingHouseViewModel //TODO: Automaper
            {
                Name = publishingHouse.Name,
                Address = publishingHouse.Address,
                //BookIds = GetPublishubgHouseBooks(publishingHouse),
            };
        }

        public List<BookViewModel> GetPublishubgHouseBooks(string Id)
        {
            List<Book> books = bookInPublishingHouseRepository.GetPublishingHouseBooks(publishingHouseRepository.GetById(Id));
            List<BookViewModel> bookViewModels = new List<BookViewModel>();
            foreach (var item in books)
            {
                bookViewModels.Add(new BookViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return bookViewModels;
        }

        //public List<string> GetPublishubgHouseBooks(string Id)
        //{
        //    List<Book> books = bookInPublishingHouseRepository.GetPublishingHouseBooks(publishingHouseRepository.GetById(Id));
        //    List<string> bookViewModels = new List<string>();
        //    foreach (var item in books)
        //    {
        //        bookViewModels.Add(item.Id);
        //    }
        //    return bookViewModels;
        //}

        public void Edit(string id, EditPublishingHouseViewModel publishingHouseViewModel)
        {
            //TODO: validation
            PublishingHouse publishingHouse = publishingHouseRepository.GetById(id);
            publishingHouse.Name = publishingHouseViewModel.Name;
            publishingHouse.Address = publishingHouseViewModel.Address;
            publishingHouseRepository.Update(publishingHouse);
        }

        public DeletePublishingHouseViewModel GetByIdDelete(string id)
        {
            PublishingHouse publishingHouse = publishingHouseRepository.GetById(id);
            return new DeletePublishingHouseViewModel
            {
                Name = publishingHouse.Name,
                Address = publishingHouse.Address,
                Books = GetPublishingHouseBooksToString(publishingHouse),
            };
        }

        public void Delete(string id)
        {
            try
            {
                publishingHouseRepository.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
