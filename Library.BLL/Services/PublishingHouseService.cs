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

        public PublishingHouseService()
        {
            db = new ApplicationContext();
            publishingHouseRepository = new PublishingHouseRepository(db);
            bookInPublishingHouseRepository = new BookInPublishingHouseRepository(db);
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
            List<Book> books = bookInPublishingHouseRepository.GetPublishingHouseBooksADO(publishingHouse.Id);
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
        

        public void Edit(string id, EditPublishingHouseViewModel publishingHouseViewModel)//TODO: refactor
        {
            //TODO: validation
            PublishingHouse publishingHouse = publishingHouseRepository.GetByID(id);
            publishingHouse.Name = publishingHouseViewModel.Name;
            publishingHouse.Address = publishingHouseViewModel.Address;
            publishingHouseRepository.Update(publishingHouse);
            
            SaveChanges();
        }
        

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
