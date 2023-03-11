using EMS.Application.DepartmentModule.Queries.GetDepartmentList;
using EMS.Application.EmployeeModule.Queries.GetEmployeeList;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ApiControllerBase
    {
        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetDepartmentListQuery()));
        }
    }
}
