using Nager.AmazonProductAdvertising;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.BookSearch.Amazon
{
    public class AmazonBookSearch : IBookSearch
    {
        public async Task<BookInfo> GetAsync(string isbn)
        {
            var authentication = new AmazonAuthentication("accessKey", "secretKey");
            var client = new AmazonProductAdvertisingClient(authentication, AmazonEndpoint.DE, "parnterTag");
            var result = await client.SearchItemsAsync(isbn);
            if (!result.Successful)
            {
                return null;
            }

            var firstBook = result.SearchResult.Items.FirstOrDefault();
            if (firstBook == null)
            {
                return null;
            }

            return new BookInfo
            {
                Title = firstBook.ItemInfo.Title.DisplayValue,
                Authors = firstBook.ItemInfo.ByLineInfo.Contributors.Where(o => o.RoleType == "author").Select(o => o.Name).ToArray(),
            };
        }
    }
}
