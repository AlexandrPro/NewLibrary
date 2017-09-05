using Library.Entities;
using System.Data.Entity;

namespace Library.DAL.Repository
{
    public class MagazineRepository : BaseRepository<Magazine>
    {
        public MagazineRepository(DbContext context) : base(context)
        {
        }
    }
}
