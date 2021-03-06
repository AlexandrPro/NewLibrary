﻿using Library.DataAccess.EF;
using Library.DataAccess.Repository;
using Library.Entities;
using Library.Entities.Enums;
using Library.ViewModels.Publication;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
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
            publicationIndexVM.publications = publicationRepository.Get().Select(x => new PublicationViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type.ToString(),
            }).ToList();
            return publicationIndexVM;
        }
    }
}
