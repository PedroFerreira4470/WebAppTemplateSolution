using Application.Common.Contracts.V1.ResponseType;
using Application.Common.Interfaces;
using Application.V1.Values.Commands.CreateValue;
using Application.V1.Values.Queries.GetValuesList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class ValuesController : BaseV1Controller
    {
        /// <summary>
        /// Return all Values in the System
        /// </summary>
        /// <remarks>
        ///  Additional detail about the method/class/field.
        /// </remarks>
        /// <param name="paginationQuery"> A parameter to the method, and what it represents.</param>
        /// <returns>All Values from the system</returns>
        /// <response code="200">Return all Values in the System</response>
        [HttpGet("{userId:int:min(1)}")]
        //[HttpGet(teste/{userId?:int:min(1)})] :regex(..):minLength(1):alpha other... router .net core
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<GetValuesListDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IRestExceptionModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IRestExceptionModel), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PagedResponse<GetValuesListDto>>> GetAllAsync([FromRoute] int userId, [FromQuery] GetValuesListQuery paginationQuery = null)
            => await Mediator.Send(paginationQuery);

        /// <summary>
        /// A high-level summary of what the method/class/field is or does.
        /// Post a new Value in the system
        /// </summary>
        /// <remarks>
        /// Additional detail about the method/class/field.
        ///  This controller is for testing purpose only.
        /// </remarks>
        /// <param name="command"> A parameter to the method, and what it represents. Dto used to Create Value Entity</param>
        /// <returns>A description of what the method returns. Returns number from the created Entity</returns>
        /// <response code="200">Post new Value in the system</response>
        /// <response code="500">Validation failed</response>
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), 200)]
        [ProducesResponseType(typeof(IRestExceptionModel), 500)]
        public async Task<ActionResult<Response<int>>> CreateValueAsync([FromBody] CreateValueCommand command)
            => await Mediator.Send(command);
    }
}
