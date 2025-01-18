using BookReview.Application.Commads.GenreCommands.Create;
using BookReview.Application.Queries.GenreQueries.GetAll;
using BookReview.Application.Queries.GenreQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var query = new GetGenreByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllGenresQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetGenreById), new { id = result.Data }, command);
        }
    }
}
