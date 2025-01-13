using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.AuthorCommands.Update
{
    public class UpdateAuthorCommand : IRequest<ResultViewModel>
    {
        public UpdateAuthorCommand(int id, string fullName, DateTime dateBirth)
        {
            Id = id;
            FullName = fullName;
            DateBirth = dateBirth;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public DateTime DateBirth { get; private set; }
    }
}
