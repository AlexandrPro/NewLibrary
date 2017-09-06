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

        public PublishingHouseService()
        {
            db = new ApplicationContext();
            publishingHouseRepository = new PublishingHouseRepository(db);
            publicationRepository = new PublicationRepository(db);
        }

        public void Create(CreatePublishingHouseViewModel item)
        {
            //TODO: validation

            Publication publication = new Publication
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                Type = "PublishingHouse"
            };
            string publicationId = publicationRepository.Create(publication);

            PublishingHouse publishingHouse = new PublishingHouse //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                Address = item.Address,
            };
            publishingHouseRepository.Create(publishingHouse);
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
                });
            }

            return publishingHouseVM;
        }

        public EditPublishingHouseViewModel GetByIdEdit(string id)
        {
            PublishingHouse publishingHouse = publishingHouseRepository.GetById(id);
            return new EditPublishingHouseViewModel //TODO: Automaper
            {
                Name = publishingHouse.Name,
                Address = publishingHouse.Address,
            };
        }

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
