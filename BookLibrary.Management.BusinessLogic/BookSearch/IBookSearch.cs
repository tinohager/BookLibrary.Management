using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.BookSearch
{
    public interface IBookSearch
    {
        Task<BookInfo> GetAsync(string isbn);
    }
}
