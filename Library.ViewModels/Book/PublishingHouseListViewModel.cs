using System.Collections.Generic;

namespace Library.ViewModels.Book
{
    public class PublishingHouseListViewModel
    {
        public List<PublishingHouseViewModel> publishingHouses;
    }

    public class PublishingHouseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
