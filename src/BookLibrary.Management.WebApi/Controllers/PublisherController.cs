using BookLibrary.Management.BusinessLogicLayer.PublisherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            this._publisherService = publisherService;
        }

        /// <summary>
        /// Get publishers
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublisherDto[]))]
        public async Task<IActionResult> GetAllAsync([FromQuery]int take = 10, [FromQuery]int skip = 0, [FromQuery]string search = null, CancellationToken cancellationToken = default)
        {
            var items = await this._publisherService.GetAllAsync(take, skip, search, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, items);
        }

        /// <summary>
        /// Get a publisher by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var item = await this._publisherService.GetAsync(id, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, item);
        }

        /// <summary>
        /// Add a publisher
        /// </summary>
        /// <param name="addItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync(AddPublisherDto addItem, CancellationToken cancellationToken)
        {
            if (await this._publisherService.AddAsync(addItem, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Update a publisher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody]UpdatePublisherDto updateItem, CancellationToken cancellationToken)
        {
            if (!await this._publisherService.ExistsAsync(id))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (await this._publisherService.UpdateAsync(id, updateItem, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}