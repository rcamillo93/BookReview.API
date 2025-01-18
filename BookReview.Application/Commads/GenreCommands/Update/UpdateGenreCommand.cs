using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.GenreCommands.Update
{
    public class UpdateGenreCommand : IRequest<ResultViewModel>
    {
        public UpdateGenreCommand(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}
