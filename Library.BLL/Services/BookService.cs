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
        BookInPublishingHouseRepository bookInPublishingHouseRepository;
        PublishingHouseRepository publishingHouseRepository;

        public BookService()
        {
            db = new ApplicationContext();
            bookRepository = new BookRepository(db);
            publicationRepository = new PublicationRepository(db);
            bookInPublishingHouseRepository = new BookInPublishingHouseRepository(db);
            publishingHouseRepository = new PublishingHouseRepository(db);
        }

        public void Create(CreateBookViewModel bookViewModel)
        {
            //TODO: validation

            Publication publication = new Publication
            {
                CreationDate = DateTime.Now,
                Name = bookViewModel.Name,
                Type = "Book"
            };
            publicationRepository.Insert(publication);

            Book book = new Book //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Author = bookViewModel.Author,
                Name = bookViewModel.Name,
                YearOfPublishing = bookViewModel.YearOfPublishing,
                PublicationId = publication.Id
            };
            bookRepository.Insert(book);
            
            if (bookViewModel.PublishingHouseIds != null)
            {
                foreach (var item in bookViewModel.PublishingHouseIds)
                {
                    bookInPublishingHouseRepository.Insert(new BookInPublishingHouse
                    {
                        Book = book,
                        PublishingHouse = publishingHouseRepository.GetByID(item),
                    });
                }
            }

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

        public PublishingHouseListViewModel GetAllPublishingHouses()
        {
            IEnumerable<PublishingHouse> publishingHouses = publishingHouseRepository.Get();
            PublishingHouseListViewModel publishingHouseListViewModels = new PublishingHouseListViewModel();
            publishingHouseListViewModels.publishingHouses = new List<PublishingHouseViewModel>();
            foreach (var item in publishingHouses)
            {
                publishingHouseListViewModels.publishingHouses.Add(new PublishingHouseViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return publishingHouseListViewModels;
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
                PublishingHouses = GetBookPublishingHousesToString(x),
            }).ToList();
            return bookIndexVM;
        }

        private string GetBookPublishingHousesToString(Book book)
        {
            List<PublishingHouse> publishingHouses = bookInPublishingHouseRepository.GetBookPublishingHouses(book.Id);
            string publishingHousesString = "";
            foreach (var item in publishingHouses)
            {
                publishingHousesString += item.Name + ", ";
            }
            if (publishingHousesString != "")
                publishingHousesString = publishingHousesString.Remove(publishingHousesString.Length - 2, 2);

            return publishingHousesString;
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

        public List<PublishingHouseViewModel> GetBookPublishubgHouses(string Id)
        {
            var publishingHouseViewModels = bookInPublishingHouseRepository.GetBookPublishingHouses(Id).
                Select(x => new PublishingHouseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            return publishingHouseViewModels;
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

            AddChangedPublishingHousesForBook(bookViewModel.PublishingHouseIds, id);

            SaveChanges();
        }

        private void AddChangedPublishingHousesForBook(List<string> changePublishingHouseIds, string id)
        {
            List<BookInPublishingHouse> bookInPublishingHouses = bookInPublishingHouseRepository.Get(b => b.Book.Id == id).ToList();
            
            RemoveAllNotChangedPublishingHouses(bookInPublishingHouses, changePublishingHouseIds);

            foreach (var item in bookInPublishingHouses)
            {
                bookInPublishingHouseRepository.Delete(item.Id);
            }

            foreach (var item in changePublishingHouseIds)
            {
                bookInPublishingHouseRepository.Insert(new BookInPublishingHouse
                {
                    Book = bookRepository.GetByID(id),
                    PublishingHouse = publishingHouseRepository.GetByID(item),
                });
            }
            SaveChanges();
        }

        private void RemoveAllNotChangedPublishingHouses(List<BookInPublishingHouse> bookInPublishingHouses, List<string> changePublishingHouseIds)
        {
            for (int i = 0; i < bookInPublishingHouses.Count; i++)
            {
                for (int j = 0; j < changePublishingHouseIds.Count; j++)
                {
                    if (bookInPublishingHouses[i].PublishingHouse.Id == changePublishingHouseIds[j])
                    {
                        bookInPublishingHouses.Remove(bookInPublishingHouses[i]);
                        changePublishingHouseIds.Remove(changePublishingHouseIds[j]);
                    }
                }
            }

            //bookInPublishingHouseRepository.Save();
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
            bookInPublishingHouseRepository.Save();
        }
    }
}
