using Library.DataAccess.EF;
using Library.DataAccess.Repository;
using Library.Entities;
using Library.Entities.Enums;
using Library.ViewModels.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
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
                Type = PublicationType.Magazin
            };
            publicationRepository.Insert(publication);

            Magazine magazine = new Magazine //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                YearOfPublishing = item.YearOfPublishing,
                Publication = publication,
            };
            magazineRepository.Insert(magazine);

            SaveChanges();
        }

        public DetailsMagazineViewModel GetByIdDetails(string id)
        {
            Magazine magazine = magazineRepository.GetByID(id);
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
            magazineIndexVM.magazines = magazineRepository.Get().Select(x => new MagazineViewModel
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
            Magazine magazine = magazineRepository.GetByID(id);
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
            Magazine magazine = magazineRepository.GetByID(id);
            magazine.Name = magazineViewModel.Name;
            magazine.Number = magazineViewModel.Number;
            magazine.YearOfPublishing = magazineViewModel.YearOfPublishing;
            magazineRepository.Update(magazine);

            Publication publication = publicationRepository.GetByID(magazine.Publication.Id);
            publication.Name = magazineViewModel.Name;
            publicationRepository.Update(publication);

            SaveChanges();
        }

        public DeleteMagazineViewModel GetByIdDelete(string id)
        {
            Magazine magazine = magazineRepository.GetByID(id);
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
                Magazine magazine = magazineRepository.GetByID(id);

                publicationRepository.Delete(magazine.Publication.Id);

                magazineRepository.Delete(id);

                SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private void SaveChanges()
        {
            magazineRepository.Save();
            publicationRepository.Save();
        }
    }
}
