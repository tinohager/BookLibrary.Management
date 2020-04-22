namespace BookLibrary.Management.BusinessLogicLayer.BookSearch
{
    public class BookInfo
    {
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string Publisher { get; set; }
        public int PublishedYear { get; set; }
        public string Description { get; set; }
    }
}
