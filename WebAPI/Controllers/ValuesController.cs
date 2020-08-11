using System;
using Application.Values.Commands.CreateValue;
using Application.Values.Queries.GetValuesList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ValuesController : BaseController
    {
        /// <summary>
        /// Return all Values in the System
        /// </summary>
        /// <response code="200">Return all Values in the System</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<ValuesListDto>), 201)]
        //[ProducesResponseType(typeof(RestException), 500)] TODO delete restException and use better exception
        public async Task<ActionResult<List<ValuesListDto>>> GetAllAsync()
        {
            return await Mediator.Send(new GetValuesListQuery());
        }

        /// <summary>
        /// Post a new Value in the system
        /// </summary>
        /// <response code="201">Post new Value in the system</response>
        /// <response code="500">Validation failed</response>
        [HttpPost]
        [ProducesResponseType(typeof(int),201)]
        //[ProducesResponseType(typeof(object), 500)]
        public async Task<ActionResult<int>> CreateValueAsync([FromBody] CreateValueCommand command)
        {
    
            return  await Mediator.Send(command);
        }
    }
}
