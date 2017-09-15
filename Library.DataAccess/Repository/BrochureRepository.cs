using Library.DataAccess.EF;
using Library.Entities;
using System.Data.Entity;

namespace Library.DataAccess.Repository
{
    public class BrochureRepository : BaseRepository<Brochure>
    {
        public BrochureRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
