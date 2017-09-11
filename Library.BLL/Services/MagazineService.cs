using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.BLL.Services
{
    public class MagazineService
    {
        ApplicationContext db;
        MagazineRepository magazineRepository;
        PublicationRepository publicationRepository;

        public MagazineService()
        {
            db = new ApplicationContext();
            magazineRepository = new MagazineRepository(db);
            publicationRepository = new PublicationRepository(db);
        }

        public void Create(CreateMagazineViewModel item)
        {
            //TODO: validation

            Publication publication = new Publication
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                Type = "Magazine"
            };
            string publicationId = publicationRepository.Create(publication);

            Magazine magazine = new Magazine //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                YearOfPublishing = item.YearOfPublishing,
                PublicationId = publicationId
            };
            magazineRepository.Create(magazine);
        }

        public DetailsMagazineViewModel GetByIdDetails(string id)
        {
            Magazine magazine = magazineRepository.GetById(id);
            return new DetailsMagazineViewModel
            {
                Name = magazine.Name,
                Number = magazine.Number,
                YearOfPublishing = magazine.YearOfPublishing
            };
        }

        public IndexMagazineViewModel GetAll()
        {
            var magazineIndexVM = new IndexMagazineViewModel();
            magazineIndexVM.magazines = magazineRepository.GetAll().Select(x => new MagazineViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
                YearOfPublishing = x.YearOfPublishing,
            }).ToList();
            return magazineIndexVM;
        }

        public EditMagazineViewModel GetByIdEdit(string id)
        {
            Magazine magazine = magazineRepository.GetById(id);
            return new EditMagazineViewModel
            {
                Name = magazine.Name,
                Number = magazine.Number,
                YearOfPublishing = magazine.YearOfPublishing
            };
        }

        public void Edit(string id, EditMagazineViewModel magazineViewModel)
        {
            //TODO: validation
            Magazine magazine = magazineRepository.GetById(id);
            magazine.Name = magazineViewModel.Name;
            magazine.Number = magazineViewModel.Number;
            magazine.YearOfPublishing = magazineViewModel.YearOfPublishing;
            magazineRepository.Update(magazine);

            Publication publication = publicationRepository.GetById(magazine.PublicationId);
            publication.Name = magazineViewModel.Name;
            publicationRepository.Update(publication);
        }

        public DeleteMagazineViewModel GetByIdDelete(string id)
        {
            Magazine magazine = magazineRepository.GetById(id);
            return new DeleteMagazineViewModel
            {
                Name = magazine.Name,
                Number = magazine.Number,
                YearOfPublishing = magazine.YearOfPublishing
            };
        }

        public void Delete(string id)
        {
            try
            {
                Magazine magazine = magazineRepository.GetById(id);

                publicationRepository.Delete(magazine.PublicationId);

                magazineRepository.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
