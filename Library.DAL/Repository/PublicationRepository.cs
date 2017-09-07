using System.Data.Entity;
using Library.Entities;
using Library.DAL.EF;

namespace Library.DAL.Repository
{
    public class PublicationRepository : BaseRepository<Publication>
    {
        public PublicationRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
