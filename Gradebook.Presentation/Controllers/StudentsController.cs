using System.Net;
using Gradebook.Application.Commands.Students.AddStudent;
using Gradebook.Application.Dtos;
using Gradebook.Application.Queries.Students.GetStudentByEmail;
using Gradebook.Application.Queries.Students.GetStudentById;
using Gradebook.Application.Queries.Students.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gradebook.Presentation.Controllers
{
    [Route("api.students")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [SwaggerOperation("Get students")]
        [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new GetStudentsQuery());
            return Ok();
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get student by Id")]
        [ProducesResponseType(typeof(StudentDto), 200)]
        public async Task<ActionResult> GetById([FromRoute]int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("[action]/{email}")]
        [SwaggerOperation("Get student by Email")]
        [ProducesResponseType(typeof(StudentDto), 200)]
        public async Task<ActionResult> GetByEmail([FromRoute] string email)
        {
            var result = await _mediator.Send(new GetStudentByEmailQuery(email));
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [SwaggerOperation("Add student")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> Post([FromBody]AddStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}
