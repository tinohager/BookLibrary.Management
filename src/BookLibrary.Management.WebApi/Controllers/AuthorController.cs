using BookLibrary.Management.BusinessLogicLayer.AuthorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }

        /// <summary>
        /// Get authors
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto[]))]
        public async Task<IActionResult> GetAllAsync([FromQuery]int take = 10, [FromQuery]int skip = 0, [FromQuery]string search = null, CancellationToken cancellationToken = default)
        {
            var items = await this._authorService.GetAllAsync(take, skip, search, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, items);
        }

        /// <summary>
        /// Get an author by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
        public async Task<IActionResult> GetAsync([FromRoute]int id, CancellationToken cancellationToken)
        {
            var item = await this._authorService.GetAsync(id, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, item);
        }

        /// <summary>
        /// Add an author
        /// </summary>
        /// <param name="addItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody]AddAuthorDto addItem, CancellationToken cancellationToken)
        {
            if (await this._authorService.AddAsync(addItem, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Update an author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody]UpdateAuthorDto updateItem, CancellationToken cancellationToken)
        {
            if (!await this._authorService.ExistsAsync(id))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (await this._authorService.UpdateAsync(id, updateItem, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}