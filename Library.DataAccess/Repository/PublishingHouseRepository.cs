using Library.Entities;
using Library.DataAccess.EF;

namespace Library.DataAccess.Repository
{
    public class PublishingHouseRepository : BaseRepository<PublishingHouse>
    {
        public PublishingHouseRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
