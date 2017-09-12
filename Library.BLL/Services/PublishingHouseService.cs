using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.PublishingHouse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.BLL.Services
{
    public class PublishingHouseService
    {
        ApplicationContext db;
        PublishingHouseRepository publishingHouseRepository;
        BookInPublishingHouseRepository bookInPublishingHouseRepository;
        //BookRepository bookRepository;

        public PublishingHouseService()
        {
            db = new ApplicationContext();
            publishingHouseRepository = new PublishingHouseRepository(db);
            bookInPublishingHouseRepository = new BookInPublishingHouseRepository(db);
            //bookRepository = new BookRepository(db);
        }

        public void Create(CreatePublishingHouseViewModel publishingHouseViewModel)
        {
            //TODO: validation
            PublishingHouse publishingHouse = new PublishingHouse //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = publishingHouseViewModel.Name,
                Address = publishingHouseViewModel.Address,
            };
            publishingHouseRepository.Insert(publishingHouse);

            //if (publishingHouseViewModel.BookIds != null)
            //{
            //    foreach (var item in publishingHouseViewModel.BookIds)
            //    {
            //        bookInPublishingHouseRepository.Insert(new BookInPublishingHouse()
            //        {
            //            Book = bookRepository.GetByID(item),
            //            PublishingHouse = publishingHouse,
            //        });
            //    }
            //}

            SaveChanges();
        }

        public DetailsPublishingHouseViewModel GetByIdDetails(string id)
        {
            PublishingHouse publishingHouse = publishingHouseRepository.GetByID(id);
            return new DetailsPublishingHouseViewModel //TODO: Automaper
            {
                Name = publishingHouse.Name,
                Address = publishingHouse.Address,
                Books = GetPublishingHouseBooksToString(publishingHouse),
            };
        }

        //public BookListViewModel GetAllBooks()
        //{
        //    IEnumerable<Book> books = bookRepository.Get(); 
        //    BookListViewModel bookListViewModels = new BookListViewModel();
        //    bookListViewModels.Books = new List<BookViewModel>();
        //    foreach (var item in books)
        //    {
        //        bookListViewModels.Books.Add(new BookViewModel()
        //        {
        //            Id = item.Id,
        //            Name = item.Name,
        //        });
        //    }
        //    return bookListViewModels;
        //}

        public IndexPublishingHouseViewModel GetAll()
        {
            var publishingHouseIndexVM = new IndexPublishingHouseViewModel();
            publishingHouseIndexVM.publishingHouses = publishingHouseRepository.Get().Select(x => new PublishingHouseViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Books = GetPublishingHouseBooksToString(x),
            }).ToList();
            return publishingHouseIndexVM;
        }

        private string GetPublishingHouseBooksToString(PublishingHouse publishingHouse)
        {
            List<Book> books = bookInPublishingHouseRepository.GetPublishingHouseBooks(publishingHouse.Id);
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
            PublishingHouse publishingHouse = publishingHouseRepository.GetByID(id);
            return new EditPublishingHouseViewModel //TODO: Automaper
            {
                Name = publishingHouse.Name,
                Address = publishingHouse.Address,
            };
        }

        //public List<BookViewModel> GetPublishubgHouseBooks(string Id)
        //{
        //    var bookViewModels = bookInPublishingHouseRepository.GetPublishingHouseBooks(Id).
        //        Select(x => new BookViewModel
        //        {
        //            Id = x.Id, 
        //            Name = x.Name,
        //        }).ToList();

        //    return bookViewModels;
        //}

        public void Edit(string id, EditPublishingHouseViewModel publishingHouseViewModel)//TODO: refactor
        {
            //TODO: validation
            PublishingHouse publishingHouse = publishingHouseRepository.GetByID(id);
            publishingHouse.Name = publishingHouseViewModel.Name;
            publishingHouse.Address = publishingHouseViewModel.Address;
            publishingHouseRepository.Update(publishingHouse);

            //AddChangedBooksForPublishingHouse(publishingHouseViewModel.BookIds, id);

            SaveChanges();
        }

        //private void AddChangedBooksForPublishingHouse(List<string> changeBookIds, string id)
        //{
            
        //    List<BookInPublishingHouse> bookInPublishingHouses = bookInPublishingHouseRepository.Get(b => b.PublishingHouse.Id == id).ToList();
        
        //    RemoveAllNotChangedBooks(bookInPublishingHouses, changeBookIds);
            
        //    foreach (var item in bookInPublishingHouses)
        //    {
        //        bookInPublishingHouseRepository.Delete(item.Id);
        //    }

        //    foreach (var item in changeBookIds)
        //    {
        //        bookInPublishingHouseRepository.Insert(new BookInPublishingHouse
        //        {
        //            Book = bookRepository.GetByID(item),
        //            PublishingHouse = publishingHouseRepository.GetByID(id),
        //        });
        //    }
        //}

        //private void RemoveAllNotChangedBooks(List<BookInPublishingHouse> bookInPublishingHouses, List<string> changeBookIds)
        //{
        //    for (int i = 0; i < bookInPublishingHouses.Count; i++)
        //    {
        //        for (int j = 0; j < changeBookIds.Count; j++)
        //        {
        //            if (bookInPublishingHouses[i].Book.Id == changeBookIds[j])
        //            {
        //                bookInPublishingHouses.Remove(bookInPublishingHouses[i]);
        //                changeBookIds.Remove(changeBookIds[j]);
        //            }
        //        }
        //    }
        //}

        public DeletePublishingHouseViewModel GetByIdDelete(string id)
        {
            PublishingHouse publishingHouse = publishingHouseRepository.GetByID(id);
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

                SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private void SaveChanges()
        {
            publishingHouseRepository.Save();
            bookInPublishingHouseRepository.Save();
        }
    }
}
