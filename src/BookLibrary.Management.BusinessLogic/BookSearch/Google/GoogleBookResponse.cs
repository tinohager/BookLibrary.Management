namespace BookLibrary.Management.BusinessLogicLayer.BookSearch.Google
{
    public class GoogleBookResponse
    {
        public string Kind { get; set; }
        public int TotalItems { get; set; }
        public GoogleBookItem[] Items { get; set; }
    }
}
