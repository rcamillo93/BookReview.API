using BookReview.Application.Commads.BookCommands.Create;
using BookReview.Application.Commads.BookCommands.Update;
using BookReview.Application.Commads.BookCommands.UpdateBookCover;
using BookReview.Application.Commads.ReviewCommans.Create;
using BookReview.Application.Commads.ReviewCommans.Delete;
using BookReview.Application.Commads.ReviewCommans.Update;
using BookReview.Application.Queries.BookQueries.GetAll;
using BookReview.Application.Queries.BookQueries.GetById;
using BookReview.Application.Queries.ReportQueries;
using BookReview.Application.Queries.ReviewQueries.GetAll;
using BookReview.Application.Queries.ReviewQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBookCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> PostColletion(List<CreateBookCommand> commands)
        {
            foreach (var command in commands)
            {
                await _mediator.Send(command);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? authorId, string?title)
        {
            var query = new GetAllBooksQuery(authorId, title);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetBookByIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateBookCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("updateImage")]
        public async Task<IActionResult> PutBookCover(UpdateBookCoverCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPost("review")]
        public async Task<IActionResult> PostReview(CreateReviewCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }


        [HttpPut("review")]
        public async Task<IActionResult> PutReview(UpdateReviewCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpGet("review/id")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var query = new GetReviewByIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("review")]
        public async Task<IActionResult> GetAllReviews(string? bookTitle)
        {
            var query = new GetAllReviewsQuery(bookTitle);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpDelete("review")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var command = new DeleteReviewCommand(reviewId);

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }


        [HttpGet("review/report")]
        public async Task<IActionResult> GetReportReviews()
        {
            var query = new GetReportQuery();

            var result = await _mediator.Send(query);

            return File(result, "application/pdf", $"RatedBooksReport-{DateTime.Now.ToShortDateString()}.pdf");
        }
    }
}
