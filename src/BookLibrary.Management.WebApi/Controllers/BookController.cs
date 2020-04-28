using BookLibrary.Management.BusinessLogicLayer.BookService;
using BookLibrary.Management.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "isbn": "978-3-499-26736-9",
        ///        "title": "Weit weg und ganz nah",
        ///        "abstract": "... dein Mann hat sich aus dem Staub gemacht.",
        ///        "publisherId": 1,
        ///        "authorIds": [ 1 ],
        ///        "bookCount": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="addItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailDto))]
        public async Task<IActionResult> AddAsync([FromBody]AddBookDto addItem, CancellationToken cancellationToken)
        {
            if (await this._bookService.CheckExistsAsync(addItem.Isbn))
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorDetailDto("Duplicate book detected"));
            }

            if (await this._bookService.AddAsync(addItem, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest, new ErrorDetailDto("Cannot add book"));
        }

        /// <summary>
        /// Get books
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailDto))]
        public async Task<IActionResult> GetAsync([FromQuery]int take = 10, [FromQuery]int skip = 0, [FromQuery]string search = null, CancellationToken cancellationToken = default)
        {
            var books = await this._bookService.GetBooksAsync(take, skip, search, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, books);
        }
    }
}