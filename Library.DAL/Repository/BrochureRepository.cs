using Library.Entities;
using System.Data.Entity;

namespace Library.DAL.Repository
{
    public class BrochureRepository : BaseRepository<Brochure>
    {
        public BrochureRepository(DbContext context) : base(context)
        {
        }
    }
}
