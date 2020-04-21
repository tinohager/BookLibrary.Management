using BookLibrary.Management.BusinessLogicLayer.CustomerService;
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
    [Authorize(Policy = "Admin")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        /// <summary>
        /// Get customers
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto[]))]
        public async Task<IActionResult> GetAllAsync([FromQuery]int take = 10, [FromQuery]int skip = 0, CancellationToken cancellationToken = default)
        {
            var items = await this._customerService.GetAllAsync(take, skip, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, items);
        }

        /// <summary>
        /// Get a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        public async Task<IActionResult> GetAsync([FromRoute]int id, CancellationToken cancellationToken = default)
        {
            var item = await this._customerService.GetAsync(id, cancellationToken);
            return StatusCode(StatusCodes.Status200OK, item);
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "gender": "female",
        ///        "firstname": "Erika",
        ///        "surname": "Mustermann",
        ///        "street": "Musterweg 1",
        ///        "postalcode": "12345",
        ///        "city": "Musterstadt",
        ///        "countryCode": "de",
        ///        "email": "erika.mustermann@example.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="addItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailDto))]
        public async Task<IActionResult> AddAsync([FromBody]AddCustomerDto addItem, CancellationToken cancellationToken)
        {
            if (await this._customerService.AddAsync(addItem, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest, new ErrorDetailDto("Cannot add customer"));
        }

        /// <summary>
        /// Deleate a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailDto))]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id, CancellationToken cancellationToken)
        {
            if (await this._customerService.DeleteAsync(id, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest, new ErrorDetailDto("Cannot delete customer"));
        }

        /// <summary>
        /// Update a customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "gender": "female",
        ///        "firstname": "Erika",
        ///        "surname": "Mustermann",
        ///        "street": "Musterweg 1",
        ///        "postalcode": "12345",
        ///        "city": "Musterstadt",
        ///        "countryCode": "de",
        ///        "email": "erika.mustermann@example.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailDto))]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody]UpdateCustomerDto updateItem, CancellationToken cancellationToken)
        {
            if (await this._customerService.UpdateAsync(id, updateItem, cancellationToken))
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest, new ErrorDetailDto("Cannot update customer"));
        }
    }
}