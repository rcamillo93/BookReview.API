using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Commads.GenreCommands.Create
{
    public class CreateGenreCommand : IRequest<ResultViewModel<int>>
    {
        public CreateGenreCommand(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }
}
