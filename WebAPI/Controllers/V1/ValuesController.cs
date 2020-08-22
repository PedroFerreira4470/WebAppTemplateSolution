using Application.Common.Contracts.V1.ResponseType;
using Application.Common.Interfaces;
using Application.V1.Values.Commands.CreateValue;
using Application.V1.Values.Queries.GetValuesList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class ValuesController : BaseControllerV1
    {

        [HttpGet("{valueId:int}")]
        public ActionResult GetValue(int valueId) => NoContent();

        //[HttpGet(../{userId:int:min(1)})] :regex(..):minLength(1):alpha other... router .net core
        //optional route params are not allowed so you need to create other controller or use fromquery
        /// <summary>
        /// Return all Values in the System
        /// </summary>
        /// <remarks>
        ///  Additional detail about the method/class/field.
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="paginationQuery"> A parameter to the method, and what it represents.</param>
        /// <returns>All Values from the system</returns>
        /// <response code="200">Return all Values in the System</response>
        [HttpGet("{userId:int:min(1)}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<GetValuesListDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IRestExceptionModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IRestExceptionModel), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllAsync([FromRoute] int userId, [FromQuery] GetValuesListQuery paginationQuery = null)
            => Ok(await Mediator.Send(paginationQuery));

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
        /// <response code="201">Post new Value in the system</response>
        /// <response code="500">Validation failed</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(Response<int>), 201)]
        [ProducesResponseType(typeof(IRestExceptionModel), 500)]
        public async Task<ActionResult> CreateValueAsync([FromBody] CreateValueCommand command)
        {
            var result = await Mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, result);
        } //Ok(await Mediator.Send(command));
          //new Uri(Request.GetEncodedUrl()+ "/" + makes.Id)
          //return Created(new Uri(Url.Link(ViewRouteName, new { taskId = taskId, id = view.Id })), view);
          //string id = // some new object id
          //var newPath = new Uri(HttpContext.Request.Path, id);
          //return this.Created(newPath);

    }
}
