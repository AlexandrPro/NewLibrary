using System.Data.Entity;
using Library.Entities;

namespace Library.DAL.Repository
{
    public class PublicationRepository : BaseRepository<Publication>
    {
        public PublicationRepository(DbContext context) : base(context)
        {
        }
    }
}
