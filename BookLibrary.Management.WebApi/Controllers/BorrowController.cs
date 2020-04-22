using BookLibrary.Management.BusinessLogicLayer.BookService;
using BookLibrary.Management.BusinessLogicLayer.BorrowService;
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
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;
        private readonly IBookService _bookService;

        public BorrowController(IBorrowService borrowService,
            IBookService bookService)
        {
            this._borrowService = borrowService;
            this._bookService = bookService;
        }

        /// <summary>
        /// Borrow a book
        /// </summary>
        /// <param name="borrowMedium"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BorrowBookAsync([FromBody]BorrowMediumDto borrowMedium, CancellationToken cancellationToken = default)
        {
            if (!await this._bookService.CheckExistsAsync(borrowMedium.BookId))
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorDetailDto("BookId is unknown"));
            }

            if (await this._borrowService.BorrowBookAsync(borrowMedium, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Return a book
        /// </summary>
        /// <param name="returnMedium"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReturnBookAsync([FromBody]ReturnMediumDto returnMedium, CancellationToken cancellationToken = default)
        {
            if (await this._borrowService.ReturnBookAsync(returnMedium, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Get outstanding Borrows
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        [Route("Outstanding")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BorrowInfoDto[]))]
        public async Task<IActionResult> GetOutstandingBorrowsAsync([FromQuery]int take = 10, [FromQuery]int skip = 0, CancellationToken cancellationToken = default)
        {
            var items = await this._borrowService.GetOutstandingBorrowsAsync(take, skip, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, items);
        }
    }
}