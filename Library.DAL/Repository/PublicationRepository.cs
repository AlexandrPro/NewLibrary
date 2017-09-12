using System.Data.Entity;
using Library.Entities;
using Library.DataAccess.EF;

namespace Library.DataAccess.Repository
{
    public class PublicationRepository : BaseRepository<Publication>
    {
        public PublicationRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
