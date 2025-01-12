using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.AuthorCommands.Create
{
    public class CreateAuthorCommand : IRequest<ResultViewModel<int>>
    {
        public CreateAuthorCommand(string fullName, DateTime dateBirth)
        {
            FullName = fullName;
            DateBirth = dateBirth;
        }

        public string FullName { get; private set; }
        public DateTime DateBirth { get; private set; }
    }
}
