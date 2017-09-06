using Library.Entities;
using System.Data.Entity;

namespace Library.DAL.Repository
{
    public class PublishingHouseRepository : BaseRepository<PublishingHouse>
    {
        public PublishingHouseRepository(DbContext context) : base(context)
        {
        }
    }
}
