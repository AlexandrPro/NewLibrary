using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.Magazine;
using System;
using System.Collections.Generic;

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
            Magazine magazine = new Magazine //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                YearOfPublishing = item.YearOfPublishing
            };
            magazineRepository.Create(magazine);

            Publication publication = new Publication
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                Type = "Magazine"
            };
            publicationRepository.Create(publication);
        }

        public IndexMagazineViewModel GetAll()
        {
            IEnumerable<Magazine> magazines = magazineRepository.GetAll();
            IndexMagazineViewModel magazineVM = new IndexMagazineViewModel();
            magazineVM.magazines = new List<MagazineViewModel>();
            foreach (Magazine item in magazines) 
            {
                magazineVM.magazines.Add(new MagazineViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    YearOfPublishing = item.YearOfPublishing
                });
                //magazineVMs.Add(magazineVM);
            }

            return magazineVM;
        }
    }
}
