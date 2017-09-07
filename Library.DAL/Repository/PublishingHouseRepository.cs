using Library.Entities;
using Library.DAL.EF;

namespace Library.DAL.Repository
{
    public class PublishingHouseRepository : BaseRepository<PublishingHouse>
    {
        public PublishingHouseRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
