namespace BookLibrary.Management.BusinessLogicLayer.BookSearch.Google
{
    public class GoogleBookItem
    {
        public string Kind { get; set; }
        public string Id { get; set; }
        public string Etag { get; set; }
        public string SelfLink { get; set; }
        public GoogleBookVolumeInfo VolumeInfo { get; set; }
    }
}
