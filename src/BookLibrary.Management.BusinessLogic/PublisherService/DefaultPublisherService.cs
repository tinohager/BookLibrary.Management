using BookLibrary.Management.DataAccessLayer;
using BookLibrary.Management.DataAccessLayer.Model;
using Mapster;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.PublisherService
{
    public class DefaultPublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public DefaultPublisherService(IPublisherRepository publisherRepository)
        {
            this._publisherRepository = publisherRepository;
        }

        public async Task<PublisherDto[]> GetAllAsync(int take, int skip, string search, CancellationToken cancellationToken = default)
        {
            var items = await this._publisherRepository.GetAllAsync(take, skip, search, cancellationToken);
            return items.Adapt<PublisherDto[]>();
        }

        public async Task<PublisherDto> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await this._publisherRepository.GetAsync(id, cancellationToken);
            return item.Adapt<PublisherDto>();
        }

        public async Task<PublisherDto> GetAsync(string name, CancellationToken cancellationToken = default)
        {
            var item = await this._publisherRepository.GetAsync(name, cancellationToken);
            return item.Adapt<PublisherDto>();
        }

        public async Task<bool> AddAsync(AddPublisherDto item, CancellationToken cancellationToken = default)
        {
            item.Name = item.Name.Trim();

            if (await this.ExistsAsync(item.Name))
            {
                return false;
            }

            return await this._publisherRepository.AddAsync(item.Adapt<Publisher>(), cancellationToken);
        }

        public async Task<bool> UpdateAsync(int id, UpdatePublisherDto item, CancellationToken cancellationToken = default)
        {
            var publisher = item.Adapt<Publisher>();
            publisher.Id = id;

            return await this._publisherRepository.UpdateAsync(publisher, cancellationToken);
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
