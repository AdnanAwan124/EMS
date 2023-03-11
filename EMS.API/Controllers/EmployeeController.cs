using EMS.Application.EmployeeModule.Commands.CreateEmployee;
using EMS.Application.EmployeeModule.Commands.DeleteEmployee;
using EMS.Application.EmployeeModule.Commands.UpdateEmployee;
using EMS.Application.EmployeeModule.Queries.GetEmployeeById;
using EMS.Application.EmployeeModule.Queries.GetEmployeeList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ApiControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetEmployeeListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery() { Id = id }));
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));

        }

        // PUT api/<EmployeeController>
        [HttpPut]
        public async Task<IActionResult> Put(UpdateEmployeeCommand command)
        {
            var responst = await Mediator.Send(command);
            return Ok(responst);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteEmployeeCommand() { Id = id }));
        }
    }
}
