﻿using Library.DAL.EF;
using Library.DAL.Repository;
using Library.Entities;
using Library.ViewModels.Publication;
using System.Collections.Generic;
using System.Linq;

namespace Library.BLL.Services
{
    public class PublicationService
    {
        ApplicationContext db;
        PublicationRepository publicationRepository;
        public PublicationService()
        {
            db = new ApplicationContext();
            publicationRepository = new PublicationRepository(db);
        }

        public IndexPublicationViewModel GetAll()
        {
            var publicationIndexVM = new IndexPublicationViewModel();
            publicationIndexVM.publications = publicationRepository.GetAll().Select(x => new PublicationViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
            }).ToList();
            return publicationIndexVM;
        }
    }
}
