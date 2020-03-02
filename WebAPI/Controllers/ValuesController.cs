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
        [HttpGet]
        public async Task<ActionResult<List<ValuesListDto>>> GetAll()
        {
            return await Mediator.Send(new GetValuesListQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateValue(CreateValueCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
