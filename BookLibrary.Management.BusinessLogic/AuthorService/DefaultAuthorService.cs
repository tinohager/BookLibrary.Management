using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Mapster;
using System.Threading;
using System.Threading.Tasks;


namespace BookLibrary.Management.BusinessLogicLayer.AuthorService
{
    public class DefaultAuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public DefaultAuthorService(IAuthorRepository authorRepository)
        {
            this._authorRepository = authorRepository;
        }

        public async Task<AuthorDto[]> GetAllAsync(int take, int skip, string search, CancellationToken cancellationToken = default)
        {
            var items = await this._authorRepository.GetAllAsync(take, skip, search, cancellationToken);
            return items.Adapt<AuthorDto[]>();
        }

        public async Task<AuthorDto> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await this._authorRepository.GetAsync(id, cancellationToken);
            return item.Adapt<AuthorDto>();
        }

        public async Task<AuthorDto> GetAsync(string name, CancellationToken cancellationToken = default)
        {
            var item = await this._authorRepository.GetAsync(name, cancellationToken);
            return item.Adapt<AuthorDto>();
        }

        public async Task<bool> AddAsync(AddAuthorDto item, CancellationToken cancellationToken = default)
        {
            item.Name = item.Name.Trim();

            if (await this.ExistsAsync(item.Name))
            {
                return false;
            }

            return await this._authorRepository.AddAsync(item.Adapt<Author>(), cancellationToken);
        }

        public async Task<bool> UpdateAsync(int id, UpdateAuthorDto item, CancellationToken cancellationToken = default)
        {
            var author = item.Adapt<Author>();
            author.Id = id;

            return await this._authorRepository.UpdateAsync(author, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await this.GetAsync(id);
            if (item == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            var item = await this.GetAsync(name);
            if (item == null)
            {
                return false;
            }

            return true;
        }
    }
}
