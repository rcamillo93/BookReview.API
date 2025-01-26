using BookReview.Application.Commads.AuthorCommands.Create;
using BookReview.Application.Commads.AuthorCommands.Update;
using BookReview.Application.Queries.AuthorQueries.GetAll;
using BookReview.Application.Queries.AuthorQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }     

        [HttpGet]
        public async Task<IActionResult> Get(string? fullName)
        {
            var query = new GetAllAuthorsQuery(fullName);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var query = new GetAuthorByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateAuthorCommand command)
        {
            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetAuthorById), new { id = result.Data }, command);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateAuthorCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
