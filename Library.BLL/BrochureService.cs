using Library.DataAccess.EF;
using Library.DataAccess.Repository;
using Library.Entities;
using Library.ViewModels.Brochure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
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
            publicationRepository.Insert(publication);

            Brochure broshure = new Brochure //TODO: automaper
            {
                CreationDate = DateTime.Now,
                Name = item.Name,
                TypeOfCover = item.TypeOfCover,
                NumberOfPages = item.NumberOfPages,
                PublicationId = publication.Id
            };
            brochureRepository.Insert(broshure);

            SaveChanges();
        }

        public DetailsBrochureViewModel GetByIdDetails(string id)
        {
            Brochure brochure = brochureRepository.GetByID(id);
            return new DetailsBrochureViewModel //TODO: Automaper
            {
                Name = brochure.Name,
                NumberOfPages = brochure.NumberOfPages,
                TypeOfCover = brochure.TypeOfCover
            };
        }

        public IndexBrochureViewModel GetAll()
        {
            var brochureIndexVM = new IndexBrochureViewModel();
            brochureIndexVM.brochures = brochureRepository.Get().Select(x => new BrochureViewModel
            {
                Id = x.Id,
                Name = x.Name,
                TypeOfCover = x.TypeOfCover,
                NumberOfPages = x.NumberOfPages

            }).ToList();
            return brochureIndexVM;
        }

        public EditBrochureViewModel GetByIdEdit(string id)
        {
            Brochure brochure = brochureRepository.GetByID(id);
            return new EditBrochureViewModel //TODO: Automaper
            {
                Name = brochure.Name,
                NumberOfPages = brochure.NumberOfPages,
                TypeOfCover = brochure.TypeOfCover
            };
        }

        public void Edit(string id, EditBrochureViewModel brochureViewModel)
        {
            Brochure brochure = brochureRepository.GetByID(id);
            brochure.Name = brochureViewModel.Name;
            brochure.NumberOfPages = brochureViewModel.NumberOfPages;
            brochure.TypeOfCover = brochureViewModel.TypeOfCover;
            brochureRepository.Update(brochure);
            
            Publication publication = publicationRepository.GetByID(brochure.PublicationId);
            publication.Name = brochureViewModel.Name;
            publicationRepository.Update(publication);

            SaveChanges();
        }

        public DeleteBrochureViewModel GetByIdDelete(string id)
        {
            Brochure brochure = brochureRepository.GetByID(id);
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
                Brochure brochure = brochureRepository.GetByID(id);

                publicationRepository.Delete(brochure.PublicationId);

                brochureRepository.Delete(id);

                SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private void SaveChanges()
        {
            brochureRepository.Save();
            publicationRepository.Save();
        }
    }
}
