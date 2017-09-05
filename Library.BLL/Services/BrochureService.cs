using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.Brochure;
using System;
using System.Collections.Generic;

namespace Library.BLL.Services
{
    public class BrochureService
    {
        ApplicationContext db;
        BrochureRepository brochureRepository;
        PublicationRepository publicationRepository;

        public BrochureService()
        {
            db = new ApplicationContext();
            brochureRepository = new BrochureRepository(db);
            publicationRepository = new PublicationRepository(db);
        }

        public void Create(CreateBrochureViewModel item)
        {
            //TODO: validation
            Brochure broshure = new Brochure //TODO: automaper
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Name = item.Name,
                TypeOfCover = item.TypeOfCover,
                NumberOfPages = item.NumberOfPages
            };
            brochureRepository.Create(broshure);

            Publication publication = new Publication
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Name = item.Name,
                Type = "Brochure"
            };
            publicationRepository.Create(publication);
        }

        public IndexBrochureViewModel GetAll()
        {
            IEnumerable<Brochure> brochures = brochureRepository.GetAll();
            IndexBrochureViewModel brochureVM = new IndexBrochureViewModel();
            brochureVM.brochures = new List<BrochureViewModel>();
            foreach (Brochure item in brochures)
            {
                brochureVM.brochures.Add(new BrochureViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    TypeOfCover = item.TypeOfCover,
                    NumberOfPages = item.NumberOfPages
                });
            }

            return brochureVM;
        }
    }
}
