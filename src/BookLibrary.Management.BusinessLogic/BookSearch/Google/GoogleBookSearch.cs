using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.BookSearch.Google
{
    public class GoogleBookSearch : IBookSearch
    {
        public async Task<BookInfo> GetAsync(string isbn)
        {
            using (var httpClient = new HttpClient())
            using (var responseMessage = await httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}"))
            {
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await responseMessage.Content.ReadAsStringAsync();
                var googleBookResponse = JsonConvert.DeserializeObject<GoogleBookResponse>(json);

                var bookInfo = googleBookResponse.Items?.Select(o => new BookInfo
                {
                    Title = o.VolumeInfo?.Title,
                    Description = o.VolumeInfo?.Description,
                    Authors = o.VolumeInfo?.Authors,
                    Publisher = o.VolumeInfo?.Publisher,
                    //PublishedYear = o.VolumeInfo?.PublishedDate
                }).FirstOrDefault();

                return bookInfo;
            }
        }
    }
}
