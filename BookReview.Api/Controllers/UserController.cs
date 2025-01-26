using BookReview.Application.Commads.UserCommands.Create;
using BookReview.Application.Commads.UserCommands.Update;
using BookReview.Application.Queries.UserQueries.GetAll;
using BookReview.Application.Queries.UserQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetUserById), new { id = result.Data }, command);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(string? fullName)
        {
            var command = new GetAllUsersQuery(fullName);

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var command = new GetUserByIdQuery(id);

            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
