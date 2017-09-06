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

            Publication publication = new Publication
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                Type = "Brochure"
            };
            string publicationId = publicationRepository.Create(publication);

            Brochure broshure = new Brochure //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                TypeOfCover = item.TypeOfCover,
                NumberOfPages = item.NumberOfPages,
                PublicationId = publicationId
            };
            brochureRepository.Create(broshure);
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

        public EditBrochureViewModel GetByIdEdit(string id)
        {
            Brochure brochure = brochureRepository.GetById(id);
            return new EditBrochureViewModel //TODO: Automaper
            {
                Name = brochure.Name,
                NumberOfPages = brochure.NumberOfPages,
                TypeOfCover = brochure.TypeOfCover
            };
        }

        public void Edit(string id, EditBrochureViewModel brochureViewModel)
        {
            Brochure brochure = brochureRepository.GetById(id);
            brochure.Name = brochureViewModel.Name;
            brochure.NumberOfPages = brochureViewModel.NumberOfPages;
            brochure.TypeOfCover = brochureViewModel.TypeOfCover;
            brochureRepository.Update(brochure);
            
            Publication publication = publicationRepository.GetById(brochure.PublicationId);
            publication.Name = brochureViewModel.Name;
            publicationRepository.Update(publication);
        }

        public DeleteBrochureViewModel GetByIdDelete(string id)
        {
            Brochure brochure = brochureRepository.GetById(id);
            return new DeleteBrochureViewModel //TODO: Automaper
            {
                Name = brochure.Name,
                NumberOfPages = brochure.NumberOfPages,
                TypeOfCover = brochure.TypeOfCover
            };
        }
        
        public void Delete(string id)
        {
            try
            {
                Brochure brochure = brochureRepository.GetById(id);

                publicationRepository.Delete(brochure.PublicationId);

                brochureRepository.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
