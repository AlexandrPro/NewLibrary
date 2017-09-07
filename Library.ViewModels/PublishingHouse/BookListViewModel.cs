using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.PublishingHouse
{
    public class BookListViewModel
    {
        public List<BookViewModel> Books { get; set; }
    }

    public class BookViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
