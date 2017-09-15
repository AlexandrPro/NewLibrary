using Library.DataAccess.EF;
using Library.Entities;
using System.Data.Entity;

namespace Library.DataAccess.Repository
{
    public class MagazineRepository : BaseRepository<Magazine>
    {
        public MagazineRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
